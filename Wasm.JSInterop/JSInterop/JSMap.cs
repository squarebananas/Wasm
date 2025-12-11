using System;
using System.Collections;
using System.Collections.Generic;

namespace nkast.Wasm.JSInterop
{
    public class JSMap<TOuterKey, TInnerKey, TValue> : CachedJSObject<JSMap<TOuterKey, TInnerKey, TValue>>,
        IReadOnlyDictionary<TOuterKey, TValue>
        where TValue : JSObject
    {
        private readonly Func<int, TValue> _getOrCreateFromUid;
        private readonly Func<TOuterKey, TInnerKey> _outerKeyToInnerKey;
        private readonly Func<TInnerKey, TOuterKey> _innerKeyToOuterKey;

        public JSMap(int uid,
            Func<int, TValue> getOrCreateFromUid,
            Func<TOuterKey, TInnerKey> outerKeyToInnerKey,
            Func<TInnerKey, TOuterKey> innerKeyToOuterKey) : base(uid)
        {
            if (getOrCreateFromUid == null)
                throw new ArgumentNullException("getOrCreateFromUid");
            if (outerKeyToInnerKey == null)
                throw new ArgumentNullException("outerKeyToInnerKey");
            if (innerKeyToOuterKey == null)
                throw new ArgumentNullException("innerKeyToOuterKey");

            _getOrCreateFromUid = getOrCreateFromUid;
            _outerKeyToInnerKey = outerKeyToInnerKey;
            _innerKeyToOuterKey = innerKeyToOuterKey;
        }

        public TValue this[TOuterKey key]
        {
            get
            {
                if (TryGetValue(key, out TValue val))
                    return val;
                throw new KeyNotFoundException();
            }
        }

        public IEnumerable<TOuterKey> Keys
        {
            get
            {
                foreach (var kv in this)
                    yield return kv.Key;
            }
        }

        public IEnumerable<TValue> Values
        {
            get
            {
                foreach (var kv in this)
                    yield return kv.Value;
            }
        }

        public int Count => InvokeRetInt("nkJSMap.GetSize");

        public virtual bool ContainsKey(TOuterKey key)
        {
            TInnerKey innerKey = _outerKeyToInnerKey(key);
            return InvokeRetBool<TInnerKey>("nkJSMap.Has", innerKey);
        }

        public bool TryGetValue(TOuterKey key, out TValue value)
        {
            TInnerKey innerKey = _outerKeyToInnerKey(key);
            int uid = InvokeRetInt<TInnerKey>("nkJSMap.Get", innerKey);

            if (uid == -1)
            {
                value = null;
                return false;
            }

            value = _getOrCreateFromUid(uid);
            return value != null;
        }

        public IEnumerator<KeyValuePair<TOuterKey, TValue>> GetEnumerator()
        {
            int iteratorUid = InvokeRetInt("nkJSMap.GetIterator");
            return new JSMapEnumerator<TOuterKey, TInnerKey, TValue>(iteratorUid, _getOrCreateFromUid, _innerKeyToOuterKey);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
