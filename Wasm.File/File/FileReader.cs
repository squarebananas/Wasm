using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.File
{
    public class FileReader : CachedJSObject<FileReader>
    {
        public event EventHandler<EventArgs> OnLoadStart;
        public event EventHandler<EventArgs> OnProgress;
        public event EventHandler<EventArgs> OnLoad;
        public event EventHandler<EventArgs> OnAbort;
        public event EventHandler<EventArgs> OnError;
        public event EventHandler<EventArgs> OnLoadEnd;

        public ReadyState ReadyState
        {
            get
            {
                return (ReadyState)InvokeRetInt("nkFileReader.ReadyState");
            }
        }

        public FileReaderResult Result
        {
            get
            {
                int uid = InvokeRetInt("nkFileReader.Result");

                FileReaderResult result = FileReaderResult.FromUid(uid);
                if (result != null)
                    return result;

                if (uid == -1)
                    return null;

                return new FileReaderResult(uid);
            }
        }

        public string Error
        {
            get
            {
                return InvokeRetString("nkFileReader.Error");
            }
        }

        public FileReader(int uid) : base(uid)
        {
            Invoke("nkFileReader.RegisterEvents");
        }

        public static FileReader Create()
        {
            int uid = JSObject.StaticInvokeRetInt("nkFileReader.Create");
            if (uid == -1)
                return null;

            FileReader fileReader = FileReader.FromUid(uid);
            if (fileReader != null)
                return fileReader;

            return new FileReader(uid);
        }

        public void ReadAsArrayBuffer(Blob blob)
        {
            Invoke("nkFileReader.ReadAsArrayBuffer", blob);
        }

        public void ReadAsBinaryString(Blob blob)
        {
            Invoke("nkFileReader.ReadAsBinaryString", blob);
        }

        public void ReadAsText(Blob blob, string encoding = null)
        {
            Invoke("nkFileReader.ReadAsText", blob, encoding);
        }

        public void ReadAsDataURL(Blob blob)
        {
            Invoke("nkFileReader.ReadAsDataURL", blob);
        }

        public void Abort()
        {
            Invoke("nkFileReader.Abort");
        }

        [JSInvokable]
        public static void JsFileReaderOnLoadStart(int uid)
        {
            FileReader fr = FileReader.FromUid(uid);
            if (fr == null)
                return;

            var handler = fr.OnLoadStart;
            if (handler != null)
                handler(fr, EventArgs.Empty);
        }

        [JSInvokable]
        public static void JsFileReaderOnProgress(int uid)
        {
            FileReader fr = FileReader.FromUid(uid);
            if (fr == null)
                return;

            var handler = fr.OnProgress;
            if (handler != null)
                handler(fr, EventArgs.Empty);
        }

        [JSInvokable]
        public static void JsFileReaderOnLoad(int uid)
        {
            FileReader fr = FileReader.FromUid(uid);
            if (fr == null)
                return;

            var handler = fr.OnLoad;
            if (handler != null)
                handler(fr, EventArgs.Empty);
        }

        [JSInvokable]
        public static void JsFileReaderOnAbort(int uid)
        {
            FileReader fr = FileReader.FromUid(uid);
            if (fr == null)
                return;

            var handler = fr.OnAbort;
            if (handler != null)
                handler(fr, EventArgs.Empty);
        }

        [JSInvokable]
        public static void JsFileReaderOnError(int uid)
        {
            FileReader fr = FileReader.FromUid(uid);
            if (fr == null)
                return;

            var handler = fr.OnError;
            if (handler != null)
                handler(fr, EventArgs.Empty);
        }

        [JSInvokable]
        public static void JsFileReaderOnLoadEnd(int uid)
        {
            FileReader fr = FileReader.FromUid(uid);
            if (fr == null)
                return;

            var handler = fr.OnLoadEnd;
            if (handler != null)
                handler(fr, EventArgs.Empty);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }

            Invoke("nkFileReader.UnregisterEvents");

            base.Dispose(disposing);
        }
    }
}
