using System;
using System.Threading.Tasks;
using nkast.Wasm.JSInterop;
using nkast.Wasm.FileSystem;

namespace nkast.Wasm.Storage
{
    public class StorageManager : CachedJSObject<StorageManager>
    {
        public StorageManager(int uid) : base(uid)
        {
        }

        public static StorageManager FromNavigator()
        {
            int uid = JSObject.StaticInvokeRetInt("nkStorageManager.Create", nkast.Wasm.Dom.Window.Current.Navigator.Uid);
            if (uid == -1)
                return null;

            StorageManager manager = StorageManager.FromUid(uid);
            if (manager != null)
                return manager;

            return new StorageManager(uid);
        }

        public Task<bool> Persisted()
        {
            int uid = InvokeRetInt("nkStorageManager.Persisted");
            PromiseBoolean promise = new PromiseBoolean(uid);
            return promise.GetTask();
        }

        public Task<bool> Persist()
        {
            int uid = InvokeRetInt("nkStorageManager.Persist");
            PromiseBoolean promise = new PromiseBoolean(uid);
            return promise.GetTask();
        }

        public Task<bool> Estimate()
        {
            int uid = InvokeRetInt("nkStorageManager.Estimate");
            PromiseBoolean promise = new PromiseBoolean(uid);
            return promise.GetTask();
        }

        public Task<FileSystemDirectoryHandle> GetDirectory()
        {
            int uid = InvokeRetInt("nkStorageManager.GetDirectory");
            Promise<FileSystemDirectoryHandle> promise = new PromiseJSObject<FileSystemDirectoryHandle>(uid,
                (int newuid) => new FileSystemDirectoryHandle(newuid));
            return promise.GetTask();
        }
    }
}
