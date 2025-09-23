using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.JSInterop;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Dom
{
    public abstract class Element<TElement> : CachedJSObject<TElement>
        where TElement : JSObject
    {
        public unsafe DOMRect GetBoundingClientRect()
        {
            DOMRect result = default;
            Invoke<IntPtr>("nkElement.GetBoundingClientRect", new IntPtr(&result));
            return result;
        }

        public int ClientLeft
        {
            get { return InvokeRetInt("nkElement.GetClientLeft"); }
        }

        public int ClientTop
        {
            get { return InvokeRetInt("nkElement.GetClientTop"); }
        }

        public int ClientWidth
        {
            get { return InvokeRetInt("nkElement.GetClientWidth"); }
        }

        public int ClientHeight
        {
            get { return InvokeRetInt("nkElement.GetClientHeight"); }
        }

        public string InnerHTML
        {
            get { return InvokeRetString("nkElement.GetInnerHTML"); }
            set { Invoke("nkElement.SetInnerHTML", value); }
        }

        protected Element(int uid) : base(uid)
        {
        }

        public TElement FirstElementChild()
        {
            int uid = InvokeRetInt("nkElement.FirstElementChild");
            if (uid == -1)
                return null;

            TElement firstElementChild = CachedJSObject<TElement>.FromUid(uid);
            if (firstElementChild != null)
                return firstElementChild;

            firstElementChild = (TElement)Activator.CreateInstance(
                typeof(TElement),
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
                null,
                new object[] { uid },
                null);

            return firstElementChild;
        }

        public void AppendChild<THTMLElement>(Element<THTMLElement> child) where THTMLElement : JSObject
        {
            Invoke("nkElement.AppendChild", child.Uid);
        }

        public void RemoveChild<THTMLElement>(Element<THTMLElement> child) where THTMLElement : JSObject
        {
            Invoke("nkElement.RemoveChild", child.Uid);
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
