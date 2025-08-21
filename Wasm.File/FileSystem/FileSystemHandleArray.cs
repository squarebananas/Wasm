using System;
using System.Collections;
using System.Collections.Generic;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.FileSystem
{
    public class FileSystemHandleArray : FileSystemHandle
       , IReadOnlyCollection<FileSystemHandle>
       , IReadOnlyList<FileSystemHandle>
    {
        internal FileSystemHandleArray(int uid) : base(uid)
        {
        }

        #region IReadOnlyList

        public FileSystemHandle this[int index]
        {
            get
            {
                int uid = InvokeRetInt("nkJSArray.GetItem", index);
                FileSystemHandle fileHandle = FileSystemHandle.FromUid(uid);
                if (fileHandle != null)
                    return fileHandle;

                if (uid == -1)
                    return null;

                FileSystemHandleKind kind = (FileSystemHandleKind)StaticInvokeRetInt("nkFileSystemHandle.Kind", uid);
                switch (kind)
                {
                    case FileSystemHandleKind.Directory:
                        return new FileSystemDirectoryHandle(uid);
                    case FileSystemHandleKind.File:
                        return new FileSystemFileHandle(uid);
                    default:
                        return null;
                }
            }
        }

        #endregion IReadOnlyList

        #region ICollection

        public int Count
        {
            get
            {
                int count = InvokeRetInt("nkJSArray.GetLength");
                return count;
            }
        }

        #endregion ICollection

        #region IEnumerable

        IEnumerator<FileSystemHandle> IEnumerable<FileSystemHandle>.GetEnumerator()
        {
            return new JSArrayEnumerator<FileSystemHandle>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<FileSystemHandle>)this).GetEnumerator();
        }

        #endregion IEnumerable
    }
}
