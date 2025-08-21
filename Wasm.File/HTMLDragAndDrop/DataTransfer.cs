using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.HTMLDragAndDrop
{
    public class DataTransfer : CachedJSObject<DataTransfer>
    {
        public DataTransferEffects DropEffect
        {
            get { return (DataTransferEffects)InvokeRetInt("nkDataTransfer.GetDropEffect"); }
            set { Invoke("nkDataTransfer.SetDropEffect", value); }
        }

        public DataTransferEffects EffectAllowed
        {
            get { return (DataTransferEffects)InvokeRetInt("nkDataTransfer.GetEffectAllowed"); }
            set { Invoke("nkDataTransfer.SetEffectAllowed", value); }
        }

        public DataTransferItemList Items
        {
            get
            {
                int uid = JSObject.StaticInvokeRetInt("nkDataTransfer.Items", Uid);
                if (uid == -1)
                    return null;

                DataTransferItemList itemList = DataTransferItemList.FromUid(uid);
                if (itemList != null)
                    return itemList;

                return new DataTransferItemList(uid);
            }
        }

        public DataTransfer(int uid) : base(uid)
        {
        }

        //public static DataTransfer Create()
        //{
        //    int uid = JSObject.StaticInvokeRetInt("nkDataTransfer.Create");
        //    if (uid == -1)
        //        return null;

        //    DataTransfer dataTransfer = DataTransfer.FromUid(uid);
        //    if (dataTransfer != null)
        //        return dataTransfer;

        //    return new DataTransfer(uid);
        //}

        public void SetDragImage(int imageUid, int x, int y)
        {
            Invoke("nkDataTransfer.SetDragImage", imageUid, x, y);
        }
    }
}
