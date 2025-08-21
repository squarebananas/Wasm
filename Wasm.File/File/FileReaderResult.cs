using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.File
{
    public class FileReaderResult : CachedJSObject<FileReaderResult> 
    {
        public FileReaderResult(int uid) : base(uid)
        {
        }

        public string AsString()
        {
            string result = InvokeRetString("nkFileReaderResult.ToString");
            return result;
        }

        public byte[] AsByteArray(byte[] byteArray)
        {
            GCHandle handle = GCHandle.Alloc(byteArray, GCHandleType.Pinned);
            try
            {
                IntPtr pData = handle.AddrOfPinnedObject();
                Invoke("nkFileReaderResult.AsByteArray", pData);
                handle.Free();
                return byteArray;
            }
            catch
            {
                handle.Free();
                throw;
            }
        }
    }
}
