using System;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Storage
{
    public class StorageEstimate : JSObject
    {
        public long Usage
        {
            get { return InvokeRetLong("nkStorageManager.EstimateUsage"); }
        }

        public long Quota
        {
            get { return InvokeRetLong("nkStorageManager.EstimateQuota"); }
        }

        internal StorageEstimate(int uid) : base(uid)
        {
        }
    }
}
