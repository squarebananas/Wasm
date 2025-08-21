using System;
using System.Threading.Tasks;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.FileSystem
{
    public class FileSystemDirectoryHandle : FileSystemHandle
    {
        public FileSystemDirectoryHandle(int uid) : base(uid)
        {
        }

        public Task<FileSystemHandleArray> GetEntries()
        {
            int uid = InvokeRetInt("nkFileSystemDirectoryHandle.GetEntries");
            Promise<FileSystemHandleArray> promise = new PromiseJSObject<FileSystemHandleArray>(uid,
                (newuid) => new FileSystemHandleArray(newuid));
            return promise.GetTask();
        }

        public Task<FileSystemFileHandle> GetFileHandle(string name, bool create)
        {
            int uid = InvokeRetInt("nkFileSystemDirectoryHandle.GetFileHandle", name, create);
            Promise<FileSystemFileHandle> promise = new PromiseJSObject<FileSystemFileHandle>(uid,
                (newuid) => new FileSystemFileHandle(newuid));
            return promise.GetTask();
        }

        public Task<FileSystemDirectoryHandle> GetDirectoryHandle(string name, bool create)
        {
            int uid = InvokeRetInt("nkFileSystemDirectoryHandle.GetDirectoryHandle", name, create);
            Promise<FileSystemDirectoryHandle> promise = new PromiseJSObject<FileSystemDirectoryHandle>(uid,
                (newuid) => new FileSystemDirectoryHandle(newuid));
            return promise.GetTask();
        }

        public Task RemoveEntry(string name, bool recursive)
        {
            int uid = InvokeRetInt("nkFileSystemDirectoryHandle.RemoveEntry", name, recursive);
            PromiseVoid promise = new PromiseVoid(uid);
            return promise.GetTask();
        }

        public Task<string> Resolve(FileSystemHandle possibleDescendant)
        {
            int uid = InvokeRetInt("nkFileSystemDirectoryHandle.Resolve", possibleDescendant.Uid);
            PromiseString promise = new PromiseString(uid);
            return promise.GetTask();
        }
    }
}
