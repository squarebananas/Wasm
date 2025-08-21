using System;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Url
{
    public class Url : CachedJSObject<Url>
    {
        public Url(int uid) : base(uid)
        {
        }

        public static string CreateObjectURL(File.File file)
        {
            string url = JSObject.StaticInvokeRetString("nkUrl.CreateObjectURL", file.Uid);
            return url;
        }

        public static void RevokeObjectURL(File.File file)
        {
            JSObject.StaticInvokeVoid("nkUrl.RevokeObjectURL", file.Uid);
        }

        public static void DownloadFromURL(string url, string filename)
        {
            JSObject.StaticInvokeVoid("nkUrl.DownloadFromURL", url, filename);
        }
    }
}
