using System;

namespace nkast.Wasm.FileSystem
{
    public class FileSystemChangeRecordEventArgs
    {
        public readonly FileSystemChangeRecord ChangeRecord;

        internal FileSystemChangeRecordEventArgs(FileSystemChangeRecord changeRecord)
        {
            this.ChangeRecord = changeRecord;
        }
    }
}
