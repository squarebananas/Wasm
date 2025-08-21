using System;
using System.Threading.Tasks;
using nkast.Wasm.File;
using nkast.Wasm.FileSystem;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.HTMLDragAndDrop
{
    public class DataTransferItem : CachedJSObject<DataTransferItem>
    {
        public DataTransferItemKind Kind
        {
            get
            {
                return (DataTransferItemKind)InvokeRetInt("nkDataTransferItem.Kind");
            }
        }

        public string Type
        {
            get
            {
                return InvokeRetString("nkDataTransferItem.Type");
            }
        }

        public DataTransferItem(int uid) : base(uid)
        {
        }

        public File.File GetAsFile()
        {
            int uid = InvokeRetInt("nkDataTransferItem.GetAsFile");
            File.File file = File.File.FromUid(uid);
            if (file != null)
                return file;

            if (uid == -1)
                return null;

            return new File.File(uid);
        }

        public Task<FileSystemHandle> GetAsFileSystemHandle()
        {
            int uid = InvokeRetInt("nkDataTransferItem.GetAsFileSystemHandle");
            Promise<FileSystemHandle> promise = new PromiseJSObject<FileSystemHandle>(uid, (newuid) =>
            {
                    FileSystemHandleKind kind = (FileSystemHandleKind)StaticInvokeRetInt("nkFileSystemHandle.Kind", newuid);
                    switch (kind)
                    {
                        case FileSystemHandleKind.Directory:
                            return new FileSystemDirectoryHandle(newuid);
                        case FileSystemHandleKind.File:
                            return new FileSystemFileHandle(newuid);
                        default:
                            return null;
                    }
                });
            return promise.GetTask();
        }

        public Task<string> GetAsString()
        {
            int uid = InvokeRetInt("nkDataTransferItem.GetAsString");
            PromiseString promise = new PromiseString(uid);
            return promise.GetTask();
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
