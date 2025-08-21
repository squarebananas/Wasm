using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.FileSystem
{
    public class FileSystemSyncAccessHandle : CachedJSObject<FileSystemSyncAccessHandle>
    {
        public FileSystemSyncAccessHandle(int uid) : base(uid)
        {
        }

        public Task<bool> ReadBlobAsArrayBuffer(byte[] data, int? at)
        {
            GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);
            try
            {
                IntPtr pData = handle.AddrOfPinnedObject();
                int uid = InvokeRetInt("nkFileSystemSyncAccessHandle.Read", pData, data.Length, (at.HasValue ? at.Value : -1));
                PromiseBoolean promise = new PromiseBoolean(uid);
                return promise.GetTask().ContinueWith(t =>
                {
                    handle.Free();
                    return t.Result;
                });
            }
            catch
            {
                handle.Free();
                throw;
            }
        }

        public Task<bool> Write(byte[] data, int? at)
        {
            GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);
            try
            {
                nint pData = handle.AddrOfPinnedObject();
                int uid = InvokeRetInt("nkFileSystemSyncAccessHandle.Write", pData, data.Length, (at.HasValue ? at.Value : -1));
                PromiseBoolean promise = new PromiseBoolean(uid);
                return promise.GetTask().ContinueWith(t =>
                {
                    handle.Free();
                    return t.Result;
                });
            }
            catch
            {
                handle.Free();
                throw;
            }
        }

        public Task Truncate(ulong size)
        {
            int uid = InvokeRetInt("nkFileSystemSyncAccessHandle.Truncate", size);
            PromiseVoid promise = new PromiseVoid(uid);
            return promise.GetTask();
        }

        public Task<long> GetSize()
        {
            int uid = InvokeRetInt("nkFileSystemSyncAccessHandle.GetSize");
            PromiseLong promise = new PromiseLong(uid);
            return promise.GetTask();
        }

        public Task Flush()
        {
            int uid = InvokeRetInt("nkFileSystemSyncAccessHandle.Flush");
            PromiseVoid promise = new PromiseVoid(uid);
            return promise.GetTask();
        }

        public Task Close()
        {
            int uid = InvokeRetInt("nkFileSystemSyncAccessHandle.Close");
            PromiseVoid promise = new PromiseVoid(uid);
            return promise.GetTask();
        }
    }
}
