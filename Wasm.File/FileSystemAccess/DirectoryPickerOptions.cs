using System;
using nkast.Wasm.FileSystem;

namespace nkast.Wasm.FileSystemAccess
{
    public struct DirectoryPickerOptions
    {
        public string Id;
        public FileSystemPermissionMode Mode;
        public FileSystemDirectoryHandle StartInFileSystemDirectoryHandle;
        public WellKnownDirectory StartInWellKnownDirectory;
    }
}
