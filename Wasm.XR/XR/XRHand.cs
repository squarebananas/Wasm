using System;
using System.Collections;
using System.Collections.Generic;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.XR
{
    public class XRHand : JSMap<XRHandJoint, int, XRJointSpace>
        , IReadOnlyDictionary<XRHandJoint, XRJointSpace>
    {
        internal XRHand(int uid) : base(uid, GetOrCreateJointSpaceFromUid, KeyToInnerKey, InnerKeyToKey)
        {
        }

        private static XRJointSpace GetOrCreateJointSpaceFromUid(int uid)
        {
            if (uid == -1)
                return null;

            XRJointSpace jointSpace = (XRJointSpace)XRJointSpace.FromUid(uid);
            if (jointSpace != null)
                return jointSpace;

            return new XRJointSpace(uid);
        }

        private static int KeyToInnerKey(XRHandJoint key)
        {
            return (int)key;
        }

        private static XRHandJoint InnerKeyToKey(int key)
        {
            return (XRHandJoint)key;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
