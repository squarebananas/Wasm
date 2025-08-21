using System;
using nkast.Wasm.FileSystem;

namespace nkast.Wasm.FileSystemAccess
{
    public struct SaveFilePickerOptions
    {
        public FilePickerOptions FilePickerOptions;
        public string SuggestedName;
    }
}
