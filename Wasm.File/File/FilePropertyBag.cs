using System;

namespace nkast.Wasm.File
{
    public struct FilePropertyBag
    {
        public BlobPropertyBag BlobPropertyBag;
        public DateTime LastModified => DateTimeOffset.FromUnixTimeMilliseconds(_lastModified).DateTime;

        private long _lastModified;
    }
}
