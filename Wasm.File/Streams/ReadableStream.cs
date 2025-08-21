using System;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Streams
{
    public class ReadableStream : CachedJSObject<ReadableStream> // to do
    {
        public ReadableStream(int uid) : base(uid)
        {
        }
    }
}
