using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using nkast.Wasm.JSInterop;
using nkast.Wasm.Streams;

namespace nkast.Wasm.File
{
    public class Blob : CachedJSObject<File>
    {
        public long Size
        {
            get
            {
                return InvokeRetLong("nkBlob.Size");
            }
        }

        public string Type
        {
            get
            {
                return InvokeRetString("nkBlob.Type");
            }
        }

        public Blob(int uid) : base(uid)
        {
        }

        public static Blob Create(BlobPart[] blobParts = null, BlobPropertyBag? options = null)
        {
            int uid = JSObject.StaticInvokeRetInt("nkBlob.Create", blobParts, options);
            if (uid == -1)
                return null;

            Blob blob = Blob.FromUid(uid);
            if (blob != null)
                return blob;

            return new Blob(uid);
        }

        public Task<Blob> Slice(long? start, long? end, string contentType = null)
        {
            int uid = InvokeRetInt("nkBlob.Slice", start, end, contentType);
            Promise<Blob> promise = new PromiseJSObject<Blob>(uid,
                (newuid) => new Blob(newuid));
            return promise.GetTask();
        }

        public ReadableStream Stream()
        {
            int uid = InvokeRetInt("nkBlob.Stream");
            if (uid == -1)
                return null;

            ReadableStream stream = ReadableStream.FromUid(uid);
            if (stream != null)
                return stream;

            return new ReadableStream(uid);
        }

        public Task<string> Text()
        {
            int uid = InvokeRetInt("nkBlob.Text");
            PromiseString promise = new PromiseString(uid);
            return promise.GetTask();
        }

        public Task ArrayBuffer() // to do
        {
            int uid = InvokeRetInt("nkBlob.ArrayBuffer");
            PromiseInt promise = new PromiseInt(uid);
            return promise.GetTask();
        }

        public Task<byte[]> Bytes() // to do
        {
            int uid = InvokeRetInt("nkBlob.Bytes");
            //PromiseBoolean promise = new PromiseBoolean(uid);
            //return promise.GetTask();
            return new Task<byte[]>(null, null);
        }
    }
}
