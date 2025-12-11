using System;
using System.Collections.Generic;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.XR
{
    public class XRJointSpace : XRSpace
    {
        public XRHandJoint JointName
        {
            get { return (XRHandJoint)InvokeRetInt("XRJointSpace.GetJointName"); }
        }

        internal XRJointSpace(int uid) : base(uid)
        {
        }
    }
}
