using System;
using System.Threading.Tasks;
using nkast.Wasm.JSInterop;
using nkast.Wasm.FileSystemAccess;
using nkast.Wasm.Permissions;

namespace nkast.Wasm.FileSystem
{
    public class FileSystemHandle : CachedJSObject<FileSystemHandle>
    {
        public FileSystemHandleKind Kind
        {
            get
            {
                return (FileSystemHandleKind)InvokeRetInt("nkFileSystemHandle.Kind");
            }
        }

        public string Name
        {
            get
            {
                return InvokeRetString("nkFileSystemHandle.Name");
            }
        }

        public FileSystemHandle(int uid) : base(uid)
        {
        }

        public Task<bool> IsSameEntry(FileSystemHandle other)
        {
            int uid = InvokeRetInt("nkFileSystemHandle.IsSameEntry", other.Uid);
            PromiseBoolean promise = new PromiseBoolean(uid);
            return promise.GetTask();
        }

        public Task<PermissionState> QueryPermissionAsync(FileSystemPermissionMode mode)
        {
            int uid = InvokeRetInt("nkFileSystem.QueryPermission", (int)mode);
            PromiseInt promise = new PromiseInt(uid);
            return promise.GetTask().ContinueWith(t =>
            {
                int state = t.Result;
                return (PermissionState)state;
            });
        }

        public Task<PermissionState> RequestPermissionAsync(FileSystemPermissionMode mode)
        {
            int uid = InvokeRetInt("nkFileSystem.RequestPermission", (int)mode);
            PromiseInt promise = new PromiseInt(uid);
            return promise.GetTask().ContinueWith(t =>
            {
                int state = t.Result;
                return (PermissionState)state;
            });
        }
    }
}
