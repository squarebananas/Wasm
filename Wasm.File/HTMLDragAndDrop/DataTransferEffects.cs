using System;

namespace nkast.Wasm.HTMLDragAndDrop
{
    public enum DataTransferEffects
    {
        None = 1,
        Copy = 2,
        CopyLink = 3,
        CopyMove = 4,
        Link = 5,
        LinkMove = 6,
        Move = 7,
        All = 8,
        Uninitialized = 9
    }
}
