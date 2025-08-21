using System;
using System.Threading.Tasks;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.FileSystem
{
    public class FileSystemFileHandle : FileSystemHandle
    {
        public FileSystemFileHandle(int uid) : base(uid)
        {
        }

        public Task<File.File> GetFile()
        {
            int uid = InvokeRetInt("nkFileSystemFileHandle.GetFile");
            Promise<File.File> promise = new PromiseJSObject<File.File>(uid, (newuid) => new File.File(newuid));
            return promise.GetTask();
        }

        public Task<FileSystemWritableFileStream> CreateWritable()
        {
            int uid = InvokeRetInt("nkFileSystemFileHandle.CreateWritable");
            Promise<FileSystemWritableFileStream> promise = new PromiseJSObject<FileSystemWritableFileStream>(uid,
                (newuid) => new FileSystemWritableFileStream(newuid));
            return promise.GetTask();
        }

        public Task<FileSystemSyncAccessHandle> CreateSyncAccessHandle()
        {
            int uid = InvokeRetInt("nkFileSystemFileHandle.CreateSyncAccessHandle");
            Promise<FileSystemSyncAccessHandle> promise = new PromiseJSObject<FileSystemSyncAccessHandle>(uid,
                (newuid) => new FileSystemSyncAccessHandle(newuid));
            return promise.GetTask();
        }
    }
}
