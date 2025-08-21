using System;
using nkast.Wasm.FileSystem;

namespace nkast.Wasm.FileSystemAccess
{
    public struct FilePickerOptions
    {
        public FilePickerAcceptType[] Types;
        public bool ExcludeAcceptAllOption;
        public string Id;
        public FileSystemDirectoryHandle StartInFileSystemDirectoryHandle;
        public WellKnownDirectory StartInWellKnownDirectory;
    }
}
