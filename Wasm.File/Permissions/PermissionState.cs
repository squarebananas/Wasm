using System;
using System.Collections.Generic;

namespace nkast.Wasm.Permissions
{
    public enum PermissionState
    {
        Granted = 1,
        Denied = 2,
        Prompt = 3
    }
}
