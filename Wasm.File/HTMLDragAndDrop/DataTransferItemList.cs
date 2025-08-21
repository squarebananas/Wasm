using System;
using System.Collections;
using System.Collections.Generic;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.HTMLDragAndDrop
{
    public class DataTransferItemList : CachedJSObject<DataTransferItemList>
    {
        public int Length
        {
            get
            {
                return InvokeRetInt("nkDataTransferItemList.Length");
            }
        }

        internal DataTransferItemList(int uid) : base(uid)
        {
        }

        public DataTransferItem this[int index]
        {
            get
            {
                int uid = InvokeRetInt("nkJSArray.GetItem", index);
                DataTransferItem fileHandle = DataTransferItem.FromUid(uid);
                if (fileHandle != null)
                    return fileHandle;

                if (uid == -1)
                    return null;

                return new DataTransferItem(uid);
            }
        }

        public void Add(File.File file)
        {
            Invoke("nkDataTransferItemList.Add", file.Uid);
        }

        public void Add(string data, string type)
        {
            Invoke("nkDataTransferItemList.Add", data, type);
        }

        public void Remove(int index)
        {
            Invoke("nkDataTransferItemList.Remove", index);
        }
        public void Clear()
        {
            Invoke("nkDataTransferItemList.Clear");
        }
    }
}
