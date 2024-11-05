﻿using System;
using System.Collections.Generic;
using nkast.Wasm.Canvas.WebGL;
using nkast.Wasm.Dom;

namespace nkast.Wasm.XR
{
    public class XRWebGLLayer : XRLayer
    {
        static Dictionary<int, WeakReference<JSObject>> _uidMap = new Dictionary<int, WeakReference<JSObject>>();

        private XRSession _xrSession;
        private IWebGLRenderingContext _glContext;

        public int FramebufferWidth
        {
            get { return InvokeRet<int>("nkXRWebGLLayer.GetFramebufferWidth"); }
        }

        public int FramebufferHeight
        {
            get { return InvokeRet<int>("nkXRWebGLLayer.GetFramebufferHeight"); }
        }

        public bool IgnoreDepthValues
        {
            get { return InvokeRet<bool>("nkXRWebGLLayer.GetIgnoreDepthValues"); }
        }

        public bool Antialias
        {
            get { return InvokeRet<bool>("nkXRWebGLLayer.GetAntialias"); }
        }

        public WebGLFramebuffer Framebuffer
        {
            get
            {
                int uid = InvokeRet<int>("nkXRWebGLLayer.GetFramebuffer");
                XRWebGLFramebuffer framebuffer = XRWebGLFramebuffer.FromUid(uid);
                if (framebuffer != null)
                    return framebuffer;

                if (uid == -1)
                    return null;

                return new XRWebGLFramebuffer(uid, _glContext);
            }
        }

        public XRWebGLLayer(XRSession xrSession, IWebGLRenderingContext glContext)
            : base(Register(xrSession, glContext))
        {
            _uidMap.Add(Uid, new WeakReference<JSObject>(this, true));

            this._xrSession = xrSession;
            this._glContext = glContext;
        }

        private static int Register(XRSession xrSession, IWebGLRenderingContext glContext)
        {
            int uid = xrSession.CreateWebGLLayer(glContext);
            return uid;
        }

        internal static XRWebGLLayer FromUid(int uid)
        {
            if (XRWebGLLayer._uidMap.TryGetValue(uid, out WeakReference<JSObject> jsObjRef))
                if (jsObjRef.TryGetTarget(out JSObject jsObj))
                    return (XRWebGLLayer)jsObj;

            return null;
        }

        public unsafe XRViewport GetViewport(XRView view)
        {
            XRViewport result = default;
            Invoke<int, IntPtr>("nkXRWebGLLayer.GetViewport", view.Uid, new IntPtr(&result));
            return result;
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