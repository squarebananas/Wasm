using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using nkast.Wasm.Dom;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.HTMLDragAndDrop
{
    public class HTMLElementExtensionDragAndDrop : CachedJSObject<HTMLElementExtensionDragAndDrop>
    {
        private JSObject _HTMLElement;

        public delegate void OnDragDropDelegate(object sender, DataTransfer dataTransfer);

        public event OnDragDropDelegate DragStart;
        public event OnDragDropDelegate Drag;
        public event OnDragDropDelegate DragEnter;
        public event OnDragDropDelegate DragLeave;
        public event OnDragDropDelegate DragOver;
        public event OnDragDropDelegate Drop;
        public event OnDragDropDelegate DragEnd;

        internal HTMLElementExtensionDragAndDrop(int uid, JSObject element) : base(uid)
        {
            _HTMLElement = element;
            Invoke("nkHTMLElement.RegisterEventsDragDrop");
        }

        internal static HTMLElementExtensionDragAndDrop Create<THTMLElement>(HTMLElement<THTMLElement> element) where THTMLElement : JSObject
        {
            int uid = JSObject.StaticInvokeRetInt("nkHTMLElement.RegisterExtensionDragAndDrop", element.Uid);
            if (uid == -1)
                return null;

            HTMLElementExtensionDragAndDrop dd = HTMLElementExtensionDragAndDrop.FromUid(uid);
            if (dd != null)
                return dd;

            return new HTMLElementExtensionDragAndDrop(uid, element);
        }

        [JSInvokable]
        public static void JsHTMLElementOnDragStart(int uid, int dataTransferId)
        {
            HTMLElementExtensionDragAndDrop extension = HTMLElementExtensionDragAndDrop.FromUid(uid);
            if (extension?.DragStart != null)
                extension.RaiseEvent(dataTransferId, extension.DragStart);
        }

        [JSInvokable]
        public static void JsHTMLElementOnDrag(int uid, int dataTransferId)
        {
            HTMLElementExtensionDragAndDrop extension = HTMLElementExtensionDragAndDrop.FromUid(uid);
            if (extension?.Drag != null)
                extension.RaiseEvent(dataTransferId, extension.Drag);
        }

        [JSInvokable]
        public static void JsHTMLElementOnDragEnter(int uid, int dataTransferId)
        {
            HTMLElementExtensionDragAndDrop extension = HTMLElementExtensionDragAndDrop.FromUid(uid);
            if (extension?.DragEnter != null)
                extension.RaiseEvent(dataTransferId, extension.DragEnter);
        }

        [JSInvokable]
        public static void JsHTMLElementOnDragLeave(int uid, int dataTransferId)
        {
            HTMLElementExtensionDragAndDrop extension = HTMLElementExtensionDragAndDrop.FromUid(uid);
            if (extension?.DragLeave != null)
                extension.RaiseEvent(dataTransferId, extension.DragLeave);
        }

        [JSInvokable]
        public static void JsHTMLElementOnDragOver(int uid, int dataTransferId)
        {
            HTMLElementExtensionDragAndDrop extension = HTMLElementExtensionDragAndDrop.FromUid(uid);
            if (extension?.DragOver != null)
                extension.RaiseEvent(dataTransferId, extension.DragOver);
        }

        [JSInvokable]
        public static void JsHTMLElementOnDrop(int uid, int dataTransferId)
        {
            HTMLElementExtensionDragAndDrop extension = HTMLElementExtensionDragAndDrop.FromUid(uid);
            if (extension?.Drop != null)
                extension.RaiseEvent(dataTransferId, extension.Drop);
        }

        [JSInvokable]
        public static void JsHTMLElementOnDragEnd(int uid, int dataTransferId)
        {
            HTMLElementExtensionDragAndDrop extension = HTMLElementExtensionDragAndDrop.FromUid(uid);
            if (extension?.DragEnd != null)
                extension.RaiseEvent(dataTransferId, extension.DragEnd);
        }

        public void RaiseEvent(int dataTransferId, OnDragDropDelegate dragDropDelegate)
        {
            DataTransfer dataTransfer = DataTransfer.FromUid(dataTransferId);
            if (dataTransfer == null)
                dataTransfer = new DataTransfer(dataTransferId);
            dragDropDelegate(_HTMLElement, dataTransfer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }

            Invoke("nkHTMLElement.UnregisterEventsDragDrop");

            base.Dispose(disposing);
        }
    }
}
