using System;

namespace nkast.Wasm.FileSystem
{
    public enum FileSystemChangeRecordType
    {
        Appeared = 1,
        Disappeared = 2,
        Errored = 3,
        Modified = 4,
        Moved = 5,
        Unknown = 6
    }
}
