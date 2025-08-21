using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using nkast.Wasm.JSInterop;
using nkast.Wasm.FileSystem;
using nkast.Wasm.FileSystemAccess;
using nkast.Wasm.HTMLDragAndDrop;

namespace nkast.Wasm.Dom
{
    public static class WindowExtensionsMethods
    {

        public static Task<FileSystemDirectoryHandle> ShowDirectoryPickerAsync(this Window window, DirectoryPickerOptions? options = null) =>
            WindowReference.Current.ShowDirectoryPickerAsync(options);

        public static Task<FileSystemHandleArray> ShowOpenFilePickerAsync(this Window window, OpenFilePickerOptions? options = null) =>
            WindowReference.Current.ShowOpenFilePickerAsync(options);

        public static Task<FileSystemFileHandle> ShowSaveFilePickerAsync(this Window window, SaveFilePickerOptions? options = null) =>
            WindowReference.Current.ShowSaveFilePickerAsync(options);
    }

    internal class WindowReference : JSObject
    {
        static Window _original;
        static WindowReference _current;

        private WindowReference(int uid) : base(uid)
        {
        }

        internal static WindowReference Current
        {
            get
            {
                if (_current == null)
                {
                    _original = Window.Current;
                    _current = new WindowReference(_original.Uid);
                }

                return _current;
            }
        }

        internal Task<FileSystemDirectoryHandle> ShowDirectoryPickerAsync(DirectoryPickerOptions? options = null)
        {
            int uid = Current.InvokeRetInt("nkWindow.ShowDirectoryPicker", options);
            Promise<FileSystemDirectoryHandle> promise = new PromiseJSObject<FileSystemDirectoryHandle>(uid,
                (int newuid) => new FileSystemDirectoryHandle(newuid));
            return promise.GetTask();
        }

        internal Task<FileSystemHandleArray> ShowOpenFilePickerAsync(OpenFilePickerOptions? options = null)
        {
            int uid = Current.InvokeRetInt("nkWindow.ShowOpenFilePicker", options);
            Promise<FileSystemHandleArray> promise = new PromiseJSObject<FileSystemHandleArray>(uid,
                (int newuid) => new FileSystemHandleArray(newuid));
            return promise.GetTask();
        }

        internal Task<FileSystemFileHandle> ShowSaveFilePickerAsync(SaveFilePickerOptions? options = null)
        {
            int uid = Current.InvokeRetInt("nkWindow.ShowSaveFilePicker", options);
            Promise<FileSystemFileHandle> promise = new PromiseJSObject<FileSystemFileHandle>(uid,
                (int newuid) => new FileSystemFileHandle(newuid));
            return promise.GetTask();
        }
    }

    public static class HTMLElementExtensionsMethods
    {
        public static HTMLElementExtensionDragAndDrop GetExtensionDragAndDrop<THTMLElement>(this HTMLElement<THTMLElement> element)
            where THTMLElement : JSObject
        {
            JSObject a = element as JSObject;

            HTMLElementExtensionDragAndDrop extension = HTMLElementExtensionDragAndDrop.Create(element);
            return extension;
        }
    }
}
