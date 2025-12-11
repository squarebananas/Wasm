using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.XR
{
    public class XRFrame : CachedJSObject<XRFrame>
    {
        private int[] _requestedUids = [];

        public XRSession Session
        {
            get
            {
                int uid = InvokeRetInt("nkXRFrame.GetSession");
                if (uid == -1)
                    return null;

                XRSession xrSession = XRSession.FromUid(uid);
                if (xrSession != null)
                    return xrSession;

                return new XRSession(uid);
            }
        }

        internal XRFrame(int uid) : base(uid)
        {
        }

        public XRViewerPose GetViewerPose(XRReferenceSpace referenceSpace)
        {
            int uid = InvokeRetInt<int>("nkXRFrame.GetViewerPose", referenceSpace.Uid);
            if (uid == -1)
                return null;

            return new XRViewerPose(uid);
        }

        public XRPose GetPose(XRSpace space, XRSpace baseSpace)
        {
            int uid = InvokeRetInt<int, int>("nkXRFrame.GetPose", space.Uid, baseSpace.Uid);
            if (uid == -1)
                return null;

            return new XRPose(uid);
        }

        public XRJointPose GetJointPose(XRJointSpace space, XRSpace baseSpace)
        {
            int uid = InvokeRetInt<int, int>("nkXRFrame.GetJointPose", space.Uid, baseSpace.Uid);
            if (uid == -1)
                return null;

            return new XRJointPose(uid);
        }

        public bool FillJointRadii(XRJointSpace[] jointSpaces, float[] radii)
        {
            if (jointSpaces.Length == 0)
                return false;
            if (radii.Length < jointSpaces.Length)
                throw new ArgumentException("radii array is smaller than jointSpaces array.");

            if (_requestedUids.Length < jointSpaces.Length)
                Array.Resize(ref _requestedUids, jointSpaces.Length);

            for (int i = 0; i < jointSpaces.Length; i++)
                _requestedUids[i] = jointSpaces[i].Uid;

            return InvokeRetBool<int, int[], float[]>("nkXRFrame.FillJointRadii", jointSpaces.Length, _requestedUids, radii);
        }

        public unsafe bool FillPoses(XRSpace[] spaces, XRSpace baseSpace, Matrix4x4[] transforms)
        {
            if (spaces.Length == 0)
                return false;
            if (transforms.Length < spaces.Length)
                throw new ArgumentException("transforms array is smaller than jointSpaces array.");

            if (_requestedUids.Length < spaces.Length)
                Array.Resize(ref _requestedUids, spaces.Length);

            for (int i = 0; i < spaces.Length; i++)
                _requestedUids[i] = spaces[i].Uid;

            return InvokeRetBool<int, int[], int, Matrix4x4[]>("nkXRFrame.FillPoses", spaces.Length, _requestedUids, baseSpace.Uid, transforms);
        }

        public unsafe Task<XRAnchor> CreateAnchorAsync(XRRigidTransform pose, XRSpace baseSpace)
        {
            int uid = InvokeRetInt<IntPtr, int>("nkXRFrame.CreateAnchor", new IntPtr(&pose), baseSpace.Uid);

            PromiseJSObject<XRAnchor> promise = new PromiseJSObject<XRAnchor>(uid,
                (int newuid) =>
                {
                    return new XRAnchor(newuid);
                });
            return promise.GetTask();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }

            base.Dispose(disposing);
        }
    }

}
