using System;
using nkast.Wasm.FileSystem;

namespace nkast.Wasm.FileSystemAccess
{
    public struct OpenFilePickerOptions
    {
        public FilePickerOptions FilePickerOptions;
        public bool Multiple;
    }
}
