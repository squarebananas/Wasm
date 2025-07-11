﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using nkast.Wasm.Dom;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.XR
{
    public class XRSystem : CachedJSObject<XRSystem>
    {

        public static XRSystem FromNavigator(Navigator navigator)
        {
            int uid = JSObject.StaticInvokeRetInt("nkXRSystem.Create", navigator.Uid);
            if (uid == -1)
                return null;

            XRSystem xrsystem = XRSystem.FromUid(uid);
            if (xrsystem != null)
                return xrsystem;

            return new XRSystem(navigator, uid);

        }

        internal XRSystem(Navigator navigator, int uid) : base(uid)
        {
            //_navigator = navigator;
        }

        public Task<bool> IsSessionSupportedAsync(string mode)
        {
            int uid = InvokeRetInt<string>("nkXRSystem.IsSessionSupported", mode);

            PromiseBoolean promise = new PromiseBoolean(uid);
            return promise.GetTask();
        }

        public Task<XRSession> RequestSessionAsync(string mode)
        {
            int uid = InvokeRetInt<string>("nkXRSystem.RequestSession", mode);

            PromiseJSObject<XRSession> promise = new PromiseJSObject<XRSession>(uid, (int newuid) => new XRSession(newuid) );
            return promise.GetTask();
        }

        public Task<XRSession> RequestSessionAsync(string mode, XRSessionOptions options)
        {
            int uid = InvokeRetInt<string, int, int>("nkXRSystem.RequestSession1", mode, (int)options.RequiredFeatures, (int)options.OptionalFeatures);

            PromiseJSObject<XRSession> promise = new PromiseJSObject<XRSession>(uid, (int newuid) => new XRSession(newuid));
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
