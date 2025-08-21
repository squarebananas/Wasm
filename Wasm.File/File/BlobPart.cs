using System;

namespace nkast.Wasm.File
{
    public struct BlobPart
    {
        public enum BlobPartType
        {
            BufferSource = 1,
            Blob = 2,
            String = 3
        }

        public BlobPartType Type;
        public int Uid;
        public string String;
    }
}
