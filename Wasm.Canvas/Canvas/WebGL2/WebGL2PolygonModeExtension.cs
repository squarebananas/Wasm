namespace nkast.Wasm.Canvas.WebGL
{
    public class WebGL2PolygonModeExtension : WebGLExtension
    {
        internal WebGL2PolygonModeExtension(int uid) : base(uid)
        {
        }

        public void PolygonMode(WebGLCullFaceMode face, WebGL2PolygonMode mode)
        {
            Invoke("nkCanvasPolygonModeExtension.PolygonMode", (int)face, (int)mode);
        }
    }
}
