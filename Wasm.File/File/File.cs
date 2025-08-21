using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.File
{
    public class File : Blob
    {
        public string Name
        {
            get
            {
                return InvokeRetString("nkFile.Name");
            }
        }

        public DateTime LastModified
        {
            get
            {
                long unixTimeMilliseconds = InvokeRetLong("nkFile.LastModified");
                return DateTimeOffset.FromUnixTimeMilliseconds(unixTimeMilliseconds).DateTime;
            }
        }

        public File(int uid) : base(uid)
        {
        }

        public static File Create(BlobPart[] fileBits, string fileName, FilePropertyBag? options = null)
        {
            int uid = JSObject.StaticInvokeRetInt("nkFile.Create", fileBits, fileName, options);
            if (uid == -1)
                return null;

            File file = File.FromUid(uid);
            if (file != null)
                return file;

            return new File(uid);
        }
    }
}
