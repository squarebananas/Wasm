﻿using System;
using System.Collections.Generic;
using System.Numerics;
using nkast.Wasm.Dom;

namespace nkast.Wasm.XR
{
    public class XRRenderState : JSObject
    {
        static Dictionary<int, WeakReference<JSObject>> _uidMap = new Dictionary<int, WeakReference<JSObject>>();

        internal XRRenderState(int uid) : base(uid)
        {
            _uidMap.Add(Uid, new WeakReference<JSObject>(this, true));
        }

        internal static XRRenderState FromUid(int uid)
        {
            if (XRRenderState._uidMap.TryGetValue(uid, out WeakReference<JSObject> jsObjRef))
                if (jsObjRef.TryGetTarget(out JSObject jsObj))
                    return (XRRenderState)jsObj;

            return null;
        }

        public unsafe float? DepthNear
        {
            get
            {
                XRViewport result = default;
                Invoke<IntPtr>("nkXRRenderState.GetDepthNear", new IntPtr(&result));
                if (result.X == -1)
                    return null;

                return result.X;
            }
        }

        public unsafe float? DepthFar
        {
            get
            {
                XRViewport result = default;
                Invoke<IntPtr>("nkXRRenderState.GetDepthFar", new IntPtr(&result));
                if (result.X == -1)
                    return null;

                return result.X;
            }
        }
        public unsafe float? InlineVerticalFieldOfView
        {
            get
            {
                XRViewport result = default;
                Invoke<IntPtr>("nkXRRenderState.GetInlineVerticalFieldOfView", new IntPtr(&result));
                if (result.X == -1)
                    return null;

                return result.X;
            }
        }

        public XRWebGLLayer BaseLayer
        {
            get
            {
                int uid = InvokeRet<int>("nkXRRenderState.GetBaseLayer");
                XRWebGLLayer glLayer = XRWebGLLayer.FromUid(uid);
                if (glLayer != null)
                    return glLayer;

                throw new NotImplementedException();
                //return new XRWebGLLayer(uid);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }

            _uidMap.Remove(Uid);

            base.Dispose(disposing);
        }
    }
}