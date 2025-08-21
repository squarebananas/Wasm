using System;

namespace nkast.Wasm.HTMLDragAndDrop
{
    public class DataTransferItemStringEventArgs
    {
        public readonly string Value;

        internal DataTransferItemStringEventArgs(string value)
        {
            Value = value;
        }
    }
}
