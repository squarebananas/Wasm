﻿using System;
using System.Collections.Generic;
using Microsoft.JSInterop;

namespace nkast.Wasm.Dom
{
    public abstract class HTMLMediaElement : JSObject, IHTMLMediaElement
    {
        static Dictionary<int, WeakReference<JSObject>> _uidMap = new Dictionary<int, WeakReference<JSObject>>();

        public event EventHandler OnEnded;
        public event EventHandler OnPlaying;
        public event EventHandler OnTimeUpdate;

        public string CurrentSrc
        {
            get { return InvokeRet<string>("nkMedia.GetCurrentSrc"); }
        }

        public string Src
        {
            get { return InvokeRet<string>("nkMedia.GetSrc"); }
            set { Invoke("nkMedia.SetSrc", value); }
        }

        public bool Ended
        {
            get { return InvokeRet<bool>("nkMedia.GetEnded"); }
        }

        public bool Paused
        {
            get { return InvokeRet<bool>("nkMedia.GetPaused"); }
        }

        public bool Muted
        {
            get { return InvokeRet<bool>("nkMedia.GetMuted"); }
            set { Invoke("nkMedia.SetMuted", value); }
        }

        public bool Loop
        {
            get { return InvokeRet<bool>("nkMedia.GetLoop"); }
            set { Invoke("nkMedia.SetLoop", value); }
        }

        public float Volume
        {
            get { throw new NotImplementedException(); }
            set { Invoke("nkMedia.SetVolume", value); }
        }

        internal HTMLMediaElement(int uid) : base(uid)
        {
            _uidMap.Add(Uid, new WeakReference<JSObject>(this, true));
            Invoke("nkMedia.RegisterEvents");
        }

        public static HTMLMediaElement FromUid(int uid)
        {
            if (HTMLMediaElement._uidMap.TryGetValue(uid, out WeakReference<JSObject> jsObjRef))
                if (jsObjRef.TryGetTarget(out JSObject jsObj))
                    return (HTMLMediaElement)jsObj;

            return null;
        }


        [JSInvokable]
        public static void JsMediaOnEnded(int uid)
        {
            HTMLMediaElement mediaElement = HTMLMediaElement.FromUid(uid);
            if (mediaElement == null)
                return;

            var handler = mediaElement.OnEnded;
            if (handler != null)
                handler(mediaElement, EventArgs.Empty);
        }

        [JSInvokable]
        public static void JsMediaOnPlaying(int uid)
        {
            HTMLMediaElement mediaElement = HTMLMediaElement.FromUid(uid);
            if (mediaElement == null)
                return;

            var handler = mediaElement.OnPlaying;
            if (handler != null)
                handler(mediaElement, EventArgs.Empty);
        }

        [JSInvokable]
        public static void JsMediaOnOnTimeUpdate(int uid)
        {
            HTMLMediaElement mediaElement = HTMLMediaElement.FromUid(uid);
            if (mediaElement == null)
                return;

            var handler = mediaElement.OnTimeUpdate;
            if (handler != null)
                handler(mediaElement, EventArgs.Empty);
        }

        public void Load()
        {
            Invoke("nkMedia.Load");
        }

        public void Play()
        {
            try
            {
                Invoke("nkMedia.Play");
            }
            catch(Exception e)
            {
                //throw;
            }
        }

        public void Pause()
        {
            Invoke("nkMedia.Pause");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }

            Pause();
            Invoke("nkMedia.UnregisterEvents");
            _uidMap.Remove(Uid);

            base.Dispose(disposing);
        }
    }
}
