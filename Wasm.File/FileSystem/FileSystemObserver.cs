using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.FileSystem
{
    public class FileSystemObserver : CachedJSObject<FileSystemObserver>
    {
        public event EventHandler<FileSystemChangeRecordEventArgs> ChangeRecord;

        internal FileSystemObserver(int uid) : base(uid)
        {
        }

        public static FileSystemObserver Create()
        {
            int uid = JSObject.StaticInvokeRetInt("nkFileSystemObserver.Create");
            if (uid == -1)
                return null;

            FileSystemObserver observer = FileSystemObserver.FromUid(uid);
            if (observer != null)
                return observer;

            return new FileSystemObserver(uid);
        }

        public Task Observe(FileSystemDirectoryHandle handle, bool recursive)
        {
            int uid = InvokeRetInt("nkFileSystemObserver.Observe", handle.Uid, recursive);
            PromiseVoid promise = new PromiseVoid(uid);
            return promise.GetTask();
        }

        public void Disconnect()
        {
            Invoke("nkFileSystemObserver.Disconnect");
        }

        [JSInvokable]
        public static void JsFileSystemObserverChangeRecord(int changeRecordUid, int observerUid)
        {
            FileSystemChangeRecord changeRecord = FileSystemChangeRecord.FromUid(changeRecordUid);
            if (changeRecord == null)
                changeRecord = new FileSystemChangeRecord(changeRecordUid);
 
            FileSystemObserver observer = FileSystemObserver.FromUid(observerUid);
            if (observer == null)
                return;

            var handler = observer.ChangeRecord;
            if (handler != null)
                handler(observer, new FileSystemChangeRecordEventArgs(changeRecord));
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
