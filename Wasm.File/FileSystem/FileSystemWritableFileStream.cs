using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using nkast.Wasm.JSInterop;
using nkast.Wasm.File;

namespace nkast.Wasm.FileSystem
{
    public class FileSystemWritableFileStream : CachedJSObject<FileSystemWritableFileStream>
    {
        public bool Locked => InvokeRetBool("nkFileSystemWritableFileStream.Locked");

        public FileSystemWritableFileStream(int uid) : base(uid)
        {
        }

        public Task<bool> Write(byte[] data)
        {
            GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);
            try
            {
                nint pData = handle.AddrOfPinnedObject();
                int uid = InvokeRetInt("nkFileSystemWritableFileStream.WriteBytes", pData, data.Length);
                PromiseVoid promise = new PromiseVoid(uid);
                return promise.GetTask().ContinueWith(t =>
                {
                    handle.Free();
                    return t.IsCompletedSuccessfully;
                });
            }
            catch
            {
                handle.Free();
                throw;
            }
        }

        public Task Write(Blob blob)
        {
            int uid = InvokeRetInt("nkFileSystemWritableFileStream.WriteBlob", blob.Uid);
            PromiseVoid promise = new PromiseVoid(uid);
            return promise.GetTask();

        }

        public Task Seek(ulong position)
        {
            int uid = InvokeRetInt("nkFileSystemWritableFileStream.Seek", position);
            PromiseVoid promise = new PromiseVoid(uid);
            return promise.GetTask();
        }

        public Task Truncate(ulong size)
        {
            int uid = InvokeRetInt("nkFileSystemWritableFileStream.Truncate", size);
            PromiseVoid promise = new PromiseVoid(uid);
            return promise.GetTask();
        }

        public Task Abort()
        {
            int uid = InvokeRetInt("nkFileSystemWritableFileStream.Abort");
            PromiseVoid promise = new PromiseVoid(uid);
            return promise.GetTask();
        }

        public Task Close()
        {
            int uid = InvokeRetInt("nkFileSystemWritableFileStream.Close");
            PromiseVoid promise = new PromiseVoid(uid);
            return promise.GetTask();
        }
    }
}
