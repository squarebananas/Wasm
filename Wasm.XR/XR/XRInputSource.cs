﻿using System;
using System.Collections.Generic;
using nkast.Wasm.Dom;
using nkast.Wasm.Input;

namespace nkast.Wasm.XR
{
    public class XRInputSource : CachedJSObject<XRInputSource>
    {

        public XRSpace GripSpace
        {
            get
            {
                int uid = InvokeRet<int>("nkXRInputSource.GetGripSpace");
                if (uid == -1)
                    return null;

                XRSpace space = XRSpace.FromUid(uid);
                if (space != null)
                    return space;

                return new XRSpace(uid);
            }
        }

        public XRSpace TargetRaySpace
        {
            get
            {
                int uid = InvokeRet<int>("nkXRInputSource.GetTargetRaySpace");
                if (uid == -1)
                    return null;

                XRSpace space = XRSpace.FromUid(uid);
                if (space != null)
                    return space;

                return new XRSpace(uid);
            }
        }

        public XRHandedness Handedness
        {
            get
            {
                int hand = InvokeRet<int>("nkXRInputSource.GetHandedness");
                return (XRHandedness)hand;
            }
        }

        public Gamepad Gamepad
        {
            get
            {
                int uid = InvokeRet<int>("nkXRInputSource.GetGamepad");
                if (uid == -1)
                    return null;

                Gamepad gamepad = Gamepad.FromUid(uid);
                if (gamepad != null)
                    return gamepad;

                return new Gamepad(uid);
            }
        }

        internal XRInputSource(int uid) : base(uid)
        {
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