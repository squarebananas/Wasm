using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace nkast.Wasm.JSInterop
{
    public class JSMapEnumerator<TOuterKey, TInnerKey, TValue> : CachedJSObject<JSMapEnumerator<TOuterKey, TInnerKey, TValue>>
        , IEnumerator<KeyValuePair<TOuterKey, TValue>>
        where TValue : JSObject
    {
        private KeyValuePair<TOuterKey, TValue> _current;

        private readonly Func<int, TValue> _getOrCreateFromUid;
        private readonly Func<TInnerKey, TOuterKey> _innerKeyToOuterKey;

        public JSMapEnumerator(int uid, Func<int, TValue> getOrCreateFromUid, Func<TInnerKey, TOuterKey> innerKeyToOuterKey) : base(uid)
        {
            _current = default;

            _getOrCreateFromUid = getOrCreateFromUid;
            _innerKeyToOuterKey = innerKeyToOuterKey;
        }

        public KeyValuePair<TOuterKey, TValue> Current => _current;

        object IEnumerator.Current => _current;

        public unsafe bool MoveNext()
        {
            bool done = false;
            TOuterKey key = default;
            int valueUid = -1;

            switch (Type.GetTypeCode(typeof(TInnerKey)))
            {
                case TypeCode.Int32:
                    {
                        int returnedKey = InvokeRetInt<IntPtr, IntPtr>("nkJSMapIterator.GetNext", new IntPtr(&done), new IntPtr(&valueUid));
                        ref TInnerKey innerRef = ref Unsafe.As<int, TInnerKey>(ref returnedKey);
                        key = _innerKeyToOuterKey(innerRef);
                        break;
                    }
                case TypeCode.String:
                    {
                        string returnedKey = InvokeRetString<IntPtr, IntPtr>("nkJSMapIterator.GetNext", new IntPtr(&done), new IntPtr(&valueUid));
                        ref TInnerKey innerRef = ref Unsafe.As<string, TInnerKey>(ref returnedKey);
                        key = _innerKeyToOuterKey(innerRef);
                        break;
                    }
                default:
                    throw new NotSupportedException($"Type '{typeof(TInnerKey)}' is not supported as map key.");
            }

            if (done)
            {
                _current = default;
                return false;
            }

            TValue value = default;
            if (valueUid != -1)
                value = _getOrCreateFromUid(valueUid);

            _current = new KeyValuePair<TOuterKey, TValue>(key, value);
            return true;
        }

        public void Reset()
        {
            throw new NotSupportedException();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _current = default;
            }

            base.Dispose(disposing);
        }
    }
}
