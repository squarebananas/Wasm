window.nkJSObject =
{
    objectMap: [null],
    emptySlots: [],
    RegisterObject: function(obj)
    {
        if (obj == null)
            return -1;
        if ('nkUid' in obj)
            throw "object already registered";
            
        // debug check
        //if (nkJSObject.objectMap.indexOf(obj) != -1)
        //    throw "object already registered";

        if (nkJSObject.emptySlots.length == 0)
        {
            nkJSObject.objectMap.push(obj);
            var uid = nkJSObject.objectMap.lastIndexOf(obj);
            obj.nkUid = uid;
            return uid;
        }
        else
        {
            var uid = nkJSObject.emptySlots.pop();

            if (nkJSObject.objectMap[uid] !== undefined)
                throw "slot allready used";

            nkJSObject.objectMap[uid] = obj;
            obj.nkUid = uid;
            return uid;
        }
    },
    GetObject: function(uid)
    {
        return nkJSObject.objectMap[uid];
    },
    GetUid: function(obj)
    {
        if (obj !== null)
        {
            if ('nkUid' in obj)
                return obj.nkUid;
        }

        return -1;
    },
    DisposeObject: function(uid)
    {
        var obj = nkJSObject.objectMap[uid];   
                
        if (obj === undefined)
            throw "obj is undefined";
        if (obj.nkUid !== uid)
            throw "invalid nkUid";

        delete obj.nkUid;
        delete nkJSObject.objectMap[uid];
        nkJSObject.emptySlots.push(uid);
    },
    GetWindow: function()
    {
        return nkJSObject.RegisterObject(window);
    },

    ReadString: function(d)
    {
        const pt = Module.HEAP32[(d)>>2];
        var str = BINDING.conv_string(pt);
        return str;
    },

    funcMap: [null],
    utf16Decoder: new TextDecoder("utf-16le"),
    ToJSString: function (pidentifier, length)
    {   
        const memory = new Uint16Array(Module.HEAPU16.buffer, pidentifier, length);
        return nkJSObject.utf16Decoder.decode(memory);
    },
    JSRegisterFunction: function (pidentifier, length)
    {
        const identifier = nkJSObject.ToJSString(pidentifier, length);

        const parts = identifier.split('.');

        let target = globalThis;
        for (let i = 0; i < parts.length - 1; i++)
        {
            target = target[parts[i]];
        }

        const functionName = parts[parts.length - 1];

        const func = target[functionName].bind(target);
        nkJSObject.funcMap.push(func);
        var fid = nkJSObject.funcMap.lastIndexOf(func);

        return fid;
    },
    JSInvoke0Int: function(fid)
    {
        let func = nkJSObject.funcMap[fid];
        return func();
    },
    JSInvoke1Void: function(fid, uid)
    {
        let func = nkJSObject.funcMap[fid];
        func(uid);
    },
    JSInvoke1Bool: function(fid, uid)
    {
        let func = nkJSObject.funcMap[fid];
        return func(uid);
    },
    JSInvoke1Int: function(fid, uid)
    {
        let func = nkJSObject.funcMap[fid];
        return func(uid);
    },
    JSInvoke1Float: function(fid, uid)
    {
        let func = nkJSObject.funcMap[fid];
        return func(uid);
    },
    JSInvoke1Double: function (fid, uid)
    {
        let func = nkJSObject.funcMap[fid];
        return func(uid);
    },
    JSInvoke1String: function(fid, uid)
    {
        let func = nkJSObject.funcMap[fid];
        return func(uid);
    },
    JSInvoke2Void: function(fid, uid, d)
    {
        let func = nkJSObject.funcMap[fid];
        func(uid, d);
    },
    JSInvoke2Bool: function(fid, uid, d)
    {
        let func = nkJSObject.funcMap[fid];
        return func(uid, d);
    },
    JSInvoke2Int: function(fid, uid, d)
    {
        let func = nkJSObject.funcMap[fid];
        return func(uid, d);
    },
    JSInvoke2Float: function(fid, uid, d)
    {
        let func = nkJSObject.funcMap[fid];
        return func(uid, d);
    },
    JSInvoke2String: function(fid, uid, d)
    {
        let func = nkJSObject.funcMap[fid];
        return func(uid, d);
    },
}

window.nkJSArray =
{
    GetLength: function (uid, d)
    {
        var ar = nkJSObject.GetObject(uid);
        return ar.length;
    },
    GetItem: function (uid, d)
    {
        var ar = nkJSObject.GetObject(uid);
        var id = Module.HEAP32[(d + 0 >> 2)];

        var it = ar[id];
        var uid = nkJSObject.GetUid(it);
        if (uid !== -1)
            return uid;

        return nkJSObject.RegisterObject(it);
    },
};

window.nkJSUInt8Array =
{
    GetLength: function (uid, d)
    {
        var ar = nkJSObject.GetObject(uid);
        return ar.length;
    },
    CopyTo: function (uid, d)
    {
        var ar = nkJSObject.GetObject(uid);
        var si = Module.HEAP32[(d+ 0)>>2];
        var di = Module.HEAP32[(d+ 4)>>2];
        var cn = Module.HEAP32[(d+ 8)>>2];
        var arr = Module.HEAP32[(d+ 12)>>2];

        var arrPtr = Blazor.platform.getArrayEntryPtr(arr, 0, 1);
        //var arrLen = Blazor.platform.getArrayLength(arr);
        var dest = new Uint8Array(Module.HEAPU8.buffer, arrPtr+di, cn);
        dest.set(ar.subarray(si, si+cn));
    },
};

window.nkJSMap =
{
    Get: function (uid, d)
    {
        var m = nkJSObject.GetObject(uid);
        var k = this.ReadKey(m, d);
        var v = m.get(k);
        if (v === undefined)
            return -1;
        var existingUid = nkJSObject.GetUid(v);
        if (existingUid !== -1)
            return existingUid;
        return nkJSObject.RegisterObject(v);
    },
    GetSize: function (uid, d)
    {
        var m = nkJSObject.GetObject(uid);
        return m.size;
    },
    GetIterator: function (uid, d)
    {
        var m = nkJSObject.GetObject(uid);
        var it = m.entries();
        it.nkJSMap = m;
        return nkJSObject.RegisterObject(it);
    },
    Has: function (uid, d)
    {
        var m = nkJSObject.GetObject(uid);
        var k = this.ReadKey(m, d);
        return m.has(k);
    },
    ReadKey: function (m, d)
    {
        var rk;
        switch (m.nkJSMapKeyType)
        {
            case "int":
                rk = Module.HEAP32[(d + 0) >> 2]; break;
            default:
                rk = nkJSObject.ReadString(d); break;
        }
        if (m.nkJSMapReadKey)
            k = m.nkJSMapReadKey(rk);
        return k;
    },
};

window.nkJSMapIterator =
{
    GetNext: function (uid, d)
    {
        var it = nkJSObject.GetObject(uid);
        var ptd = Module.HEAP32[(d + 0) >> 2];
        var ptv = Module.HEAP32[(d + 4) >> 2];

        var r = it.next();
        var d = r.done === true;

        var k, v;
        if (!d)
        {
            if (r.value[0])
                k = r.value[0];

            if (r.value[1])
                v = r.value[1];

            var vid;
            if (v)
            {
                vid = nkJSObject.GetUid(v);
                if (vid == -1)
                    vid = nkJSObject.RegisterObject(v);
            }
            else
            {
                vid = -1;
            }
        }

        Module.HEAP32[(ptd + 0) >> 2] = d ? 1 : 0;
        Module.HEAP32[(ptv + 0) >> 2] = vid;

        if (k)
        {
            var wk = it.nkJSMap.nkJSMapWriteKey(k);
            return wk;
        }
        else
        {
            switch (it.nkJSMap.nkJSMapKeyType)
            {
                case "int":
                    return -1;
                default:
                    return null;
            }
        }
    },
};

window.nkPromise =
{
    GetValueBoolean: function (uid)
    {
        var pr = nkJSObject.GetObject(uid);
        return pr.AsyncValue;
    },
    GetValueString: function (uid)
    {
        var pr = nkJSObject.GetObject(uid);
        return pr.AsyncValue;
    },
    GetValueJSObject: function (uid)
    {
        var pr = nkJSObject.GetObject(uid);

        var ob = pr.AsyncValue;
        var uid = nkJSObject.GetUid(ob);
        if (uid !== -1)
            return uid;

        return nkJSObject.RegisterObject(ob);
    },
    GetErrorType: function (uid)
    {
        var pr = nkJSObject.GetObject(uid);

        if (pr.Error instanceof DOMException)
        {
            switch (pr.Error.name)
            {
                case "InvalidStateError":
                    return 11;
                case "NotSupportedError":
                    return 12;
                case "SecurityError":
                    return 13;
                case "NotAllowedError":
                    return 14;
                case "AbortError":
                    return 15;

                default:
                    return 10;
            }
        }
        else if (pr.Error instanceof Error)
        {
            return 2;
        }
        else if (typeof pr.Error === "string")
        {
            return 1;
        }
        else
        {
            return 0;
        }
    },
    GetErrorMessage: function (uid)
    {
        var pr = nkJSObject.GetObject(uid);

        if (pr.Error instanceof DOMException)
        {
            return pr.Error.message;
        }
        else if (pr.Error instanceof Error)
        {
            return pr.Error.message;
        }
        else if (typeof pr.Error === "string")
        {
            return pr.Error;
        }
        else
        {
            return "Unknown Error";
        }
    },

    RegisterEvents: function (uid)
    {
        var pr = nkJSObject.GetObject(uid);

        pr.then((value) =>
        {
            pr.AsyncValue = value;
            DotNet.invokeMethod('nkast.Wasm.JSInterop', 'JsPromiseOnCompleted', uid);
        }
        ).catch((error) =>
        {
            pr.Error = error;
            DotNet.invokeMethod('nkast.Wasm.JSInterop', 'JsPromiseOnError', uid);
        });
    },
};
