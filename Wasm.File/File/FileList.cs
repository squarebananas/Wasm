using System;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.File
{
    public class FileList : CachedJSObject<FileList>
    {
        public long Length
        {
            get
            {
                return InvokeRetLong("window.nkFileList.Length");
            }
        }

        public FileList(int uid) : base(uid)
        {
        }

        public File GetItem(int index)
        {
            int uid = InvokeRetInt("window.nkFileList.GetItem", index);

            File file = File.FromUid(uid);
            if (file != null)
                return file;

            if (uid == -1)
                return null;

            return new File(uid);
        }
    }
}
