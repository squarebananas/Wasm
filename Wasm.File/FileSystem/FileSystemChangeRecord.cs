using System;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.FileSystem
{
    public class FileSystemChangeRecord : CachedJSObject<FileSystemChangeRecord>
    {
        public FileSystemHandle ChangedHandle
        {
            get
            {
                int uid = InvokeRetInt("nkFileSystemChangeRecord.ChangedHandle");
                if (uid == -1)
                    return null;

                FileSystemHandle fileSystemHandle = FileSystemHandle.FromUid(uid);
                if (fileSystemHandle != null)
                    return fileSystemHandle;

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

        public string[] RelativePathComponents
        {
            get
            {
                string relativePath = InvokeRetString("nkFileSystemChangeRecord.RelativePathComponents");
                string[] components = relativePath.Split('/');
                return components;
            }
        }

        public string[] RelativePathMovedFrom
        {
            get
            {
                string relativePath = InvokeRetString("nkFileSystemChangeRecord.RelativePathMovedFrom");
                string[] components = relativePath.Split('/');
                return components;
            }
        }

        public FileSystemHandle Root
        {
            get
            {
                int uid = InvokeRetInt("nkFileSystemChangeRecord.Root");
                return FileSystemHandle.FromUid(uid);
            }
        }

        public FileSystemChangeRecordType Type
        {
            get
            {
                int type = InvokeRetInt("nkFileSystemChangeRecord.Type");
                return (FileSystemChangeRecordType)type;
            }
        }

        public FileSystemChangeRecord(int uid) : base(uid)
        {
        }
    }
}
