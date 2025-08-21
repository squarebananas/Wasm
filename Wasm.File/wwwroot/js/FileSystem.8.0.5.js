window.nkBlob =
{
    Create: function (d)
    {
        var bpid = Module.HEAP32[(d + 0) >> 2];
        var bp = nkJSObject.GetObject(bpid);
        var pb = this.ObjectBlobPropertyBag(d + 4);
        var b;
        if (!bp && !pb)
            b = new Blob();
        else if (!pb)
            b = new Blob(bp);
        else if (!bp)
            b = new Blob(pb);
        else
            b = new Blob(bp, pb);
        return nkJSObject.RegisterObject(b);
    },
    Size: function (uid)
    {
        var b = nkJSObject.GetObject(uid);
        return b.size;
    },
    Type: function (uid)
    {
        var b = nkJSObject.GetObject(uid);
        return b.type;
    },
    Slice: function (uid, d)
    {
        var b = nkJSObject.GetObject(uid);
        var st = Module.HEAP32[(d + 0) >> 2];
        var en = Module.HEAP32[(d + 4) >> 2];
        var ty = nkJSObject.ReadString(d + 8);
        var sl;
        if (!st)
            sl = b.slice();
        else if (!en)
            sl = b.slice(st);
        else if (!ty)
            sl = b.slice(st, en);
        else
            sl = b.slice(st, en, ty);
        return sl;
    },
    Stream: function (uid)
    {
        var b = nkJSObject.GetObject(uid);
        var st = b.stream();
        return nkJSObject.RegisterObject(st);
    },
    Text: function (uid)
    {
        var b = nkJSObject.GetObject(uid);
        var pr = b.text();
        return nkJSObject.RegisterObject(pr);
    },
    ArrayBuffer: function (uid)
    {
        var b = nkJSObject.GetObject(uid);
        var pr = b.arrayBuffer();
        return nkJSObject.RegisterObject(pr);
    },
    Bytes: function (uid)
    {
        var b = nkJSObject.GetObject(uid);
        var pr = b.bytes();
        return nkJSObject.RegisterObject(pr);
    },
    EnumEndingType: function (d)
    {
        var et = Module.HEAP32[d >> 2];
        switch (et)
        {
            case 1: return "transparent";
            case 2: return "native";
            default: return null;
        }
    },
    ObjectBlobPropertyBag: function (d)
    {
        var ty = nkJSObject.ReadString(d);
        var en = this.EnumEndingType(d + 4);
        var pb =
        {
            type: ty,
            endings: en
        };
        return pb;
    },
    ObjectBlobParts: function (d)
    {
        var arr = Module.HEAP32[(d + 0) >> 2];
        var arrPtr = Blazor.platform.getArrayEntryPtr(arr, 0, 4);
        var arrLen = Blazor.platform.getArrayLength(arr);
        var bps = [];
        for (var i = 0; i < arrLen; i++)
        {
            var bpid = Module.HEAP32[(arrPtr + (i * 4)) >> 2];
            var bp = nkJSObject.GetObject(bpid);
            if ((typeof bp !== "BufferSource") && (typeof bp !== "Blob"))
                bp = nkJSObject.ReadString(arrPtr + (i * 4));
            bps.push(bp);
        }
        return bps;
    }
};

window.nkFile =
{
    Create: function (d)
    {
        var fbid = Module.HEAP32[(d + 0) >> 2];
        var fb = nkJSObject.GetObject(fbid);
        var fn = nkJSObject.ReadString(d + 4);
        var pb = this.ObjectBlobPropertyBag(d + 8);
        var f;
        if (pb)
            f = new File(fb, fn, pb);
        else
            f = new File(fb, fn);
        return nkJSObject.RegisterObject(f);
    },
    Name: function (uid)
    {
        var f = nkJSObject.GetObject(uid);
        return f.name;
    },
    LastModified: function (uid)
    {
        var f = nkJSObject.GetObject(uid);
        return f.lastModified;
    },
    ObjectFilePropertyBag: function (d)
    {
        var ty = nkJSObject.ReadString(d);
        var en = EnumEndingType(d + 4);
        var lm = Module.HEAP32[(d + 8) >> 2];
        var pb =
        {
            type: ty,
            endings: en,
            lastModified: lm
        };
        return pb;
    }
};

window.nkFileList =
{
    GetItem: function (uid, d)
    {
        var fl = nkJSObject.GetObject(uid);
        var ix = Module.HEAP32[(d + 0) >> 2];
        var fi = fl[ix]
        var fiid = nkJSObject.GetUid(fi);
        if (fiid !== -1)
            return fiid;
        return nkJSObject.RegisterObject(fi);
    },
    Length: function (uid)
    {
        var fl = nkJSObject.GetObject(uid);
        return fl.length;
    }
};

window.nkFileReader =
{
    Create: function ()
    {
        var fr = new FileReader();
        return nkJSObject.RegisterObject(fr);
    },
    ReadAsArrayBuffer: function (uid, d)
    {
        var fr = nkJSObject.GetObject(uid);
        var b = nkJSObject.GetObject(d);
        fr.readAsArrayBuffer(b);
    },
    ReadAsBinaryString: function (uid, d)
    {
        var fr = nkJSObject.GetObject(uid);
        var b = nkJSObject.GetObject(d);
        fr.readAsBinaryString(b);
    },
    ReadAsText: function (uid, d)
    {
        var fr = nkJSObject.GetObject(uid);
        var b = nkJSObject.GetObject(d);
        var e = nkJSObject.GetObject(d + 4);
        if (e)
            fr.readAsText(b, e);
        else
            fr.readAsText(b);
    },
    ReadAsDataURL: function (uid, d)
    {
        var fr = nkJSObject.GetObject(uid);
        var b = nkJSObject.GetObject(d);
        fr.readAsDataURL(b);
    },
    Abort: function (uid)
    {
        var fr = nkJSObject.GetObject(uid);
        fr.abort();
    },
    ReadyState: function (uid)
    {
        var fr = nkJSObject.GetObject(uid);
        switch (fr.readyState)
        {
            case "EMPTY": return 0;
            case "LOADING": return 1;
            case "DONE": return 2;
            default: return null;
        }
    },
    Result: function (uid, d)
    {
        var fr = nkJSObject.GetObject(uid);
        var pt = Module.HEAP32[(d + 0) >> 2];
        if (fr.result instanceof ArrayBuffer)
        {
            var len = new Uint8Array(fr.result.length);
            var arr = new Uint8Array(fr.result);
            Module.HEAPU8.set(arr, pt);
        }
        else if (typeof fr.result === "string")
        {
            return fr.result;
        }
        return null;
    },
    Error: function (uid)
    {
        var fr = nkJSObject.GetObject(uid);
        return (fr.error) ? fr.error.message : null;
    },
    RegisterEvents: function (uid)
    {
        var fr = nkJSObject.GetObject(uid);
        fr.onloadstart = function (event)
        {
            DotNet.invokeMethod('nkast.Wasm.Dom', 'JsFileReaderOnLoadStart', uid);
        };
        fr.onprogress = function (event)
        {
            DotNet.invokeMethod('nkast.Wasm.Dom', 'JsFileReaderOnProgress', uid);
        };
        fr.onload = function (event)
        {
            DotNet.invokeMethod('nkast.Wasm.Dom', 'JsFileReaderOnLoad', uid);
        };
        fr.onabort = function (event)
        {
            DotNet.invokeMethod('nkast.Wasm.Dom', 'JsFileReaderOnAbort', uid);
        };
        fr.onerror = function (event)
        {
            DotNet.invokeMethod('nkast.Wasm.Dom', 'JsFileReaderOnError', uid);
        };
        fr.onloadend = function (event)
        {
            DotNet.invokeMethod('nkast.Wasm.Dom', 'JsFileReaderOnLoadEnd', uid);
        };
    },
    UnregisterEvents: function (uid)
    {
        var fr = nkJSObject.GetObject(uid);
        fr.onloadstart = null;
        fr.onprogress = null;
        fr.onload = null;
        fr.onabort = null;
        fr.onerror = null;
        fr.onloadend = null;
    }
};

window.nkFileReaderResult =
{
    AsByteArray: function (uid, d)
    {
        var fr = nkJSObject.GetObject(uid);
        var pt = Module.HEAP32[(d + 0) >> 2];
        var arr = new Uint8Array(fr.result);
        Module.HEAPU8.set(arr, pt);
        return null;
    },
    AsString: function (uid)
    {
        var fr = nkJSObject.GetObject(uid);
        return fr.result;
    }
}

window.nkDataTransfer =
{
    Create: function ()
    {
        var dt = new DataTransfer();
        return nkJSObject.RegisterObject(dt);
    },
    EffectsToString: function (d)
    {
        var ef = Module.HEAP32[d >> 2];
        switch (ef)
        {
            case 1: return "none";
            case 2: return "copy";
            case 3: return "copyLink";
            case 4: return "copyMove";
            case 5: return "link";
            case 6: return "linkMove";
            case 7: return "move";
            case 8: return "all";
            case 9: return "uninitialized";
            default: return null;
        }
    },
    EffectsToInt: function (ef)
    {
        switch (ef)
        {
            case "none": return 1;
            case "copy": return 2;
            case "copyLink": return 3;
            case "copyMove": return 4;
            case "link": return 5;
            case "linkMove": return 6;
            case "move": return 7;
            case "all": return 8;
            case "uninitialized": return 9;
            default: return null;
        }
    },
    GetDropEffect: function (uid)
    {
        var dt = nkJSObject.GetObject(uid);
        var de = this.EffectsToInt(dt.dropEffect);
        return de;
    },
    SetDropEffect: function (uid, d)
    {
        var dt = nkJSObject.GetObject(uid);
        var de = this.EffectsToString(d);
        dt.dropEffect = de;
    },
    GetEffectAllowed: function (uid)
    {
        var dt = nkJSObject.GetObject(uid);
        var ea = this.EffectsToInt(dt.effectAllowed);
        return ea;
    },
    SetEffectAllowed: function (uid, d)
    {
        var dt = nkJSObject.GetObject(uid);
        var ea = this.EffectsToString(d);
        dt.effectAllowed = ea;
    },
    Items: function (uid)
    {
        var dt = nkJSObject.GetObject(uid);
        var dtl = dt.items;
        var lid = nkJSObject.GetUid(dtl);
        if (lid !== -1)
            return lid;
        return nkJSObject.RegisterObject(dtl);
    },
    SetDragImage: function (uid, d)
    {
        var dt = nkJSObject.GetObject(uid);
        var iid = Module.HEAP32[(d + 0) >> 2];
        var i = nkJSObject.GetObject(iid);
        var x = Module.HEAP32[(d + 0) >> 2];
        var y = Module.HEAP32[(d + 4) >> 2];
        return dt.SetDragImage(i, x, y);
    }
};

window.nkDataTransferItemList =
{
    Length: function (uid)
    {
        var dtl = nkJSObject.GetObject(uid);
        return dtl.length;
    },
    Item: function (uid, d)
    {
        var dtl = nkJSObject.GetObject(uid);
        var ix = Module.HEAP32[(d + 0) >> 2];
        var dti = dtl[ix];
        return nkJSObject.RegisterObject(dti);
    },
    Add: function (uid, d)
    {
        var dtl = nkJSObject.GetObject(uid);
        var iid = Module.HEAP32[(d + 0) >> 2];
        var dti = nkJSObject.GetObject(iid);
        if (typeof dti == "file")
        {
            dtl.push(dti);
        }
        else
        {
            var da = nkJSObject.ReadString(d + 4);
            var ty = nkJSObject.ReadString(d + 8);
            dtl.push(da, ty);
        }
    },
    Remove: function (uid, d)
    {
        var dtl = nkJSObject.GetObject(uid);
        var dti = Module.HEAP32[(d + 0) >> 2];
        dtl.remove(dti);
    },
    Clear: function (uid)
    {
        var dtl = nkJSObject.GetObject(uid);
        dtl.clear();
    }
};

window.nkDataTransferItem =
{
    Kind: function (uid)
    {
        var dti = nkJSObject.GetObject(uid);
        switch (dti.kind)
        {
            case "string": return 1;
            case "file": return 2;
            default: return null;
        }
    },
    Type: function (uid)
    {
        var dti = nkJSObject.GetObject(uid);
        return dti.type;
    },
    GetAsString: function (uid)
    {
        var dti = nkJSObject.GetObject(uid);
        var pr = new Promise((resolve) =>
        {
            var cb = (s) => resolve(s);
            dti.getAsString(cb);
        });
        return nkJSObject.RegisterObject(pr);
    },
    GetAsFile: function (uid)
    {
        var dti = nkJSObject.GetObject(uid);
        var fi = dti.getAsFile();
        return nkJSObject.RegisterObject(fi);
    },
    GetAsFileSystemHandle: function (uid)
    {
        var dti = nkJSObject.GetObject(uid);
        var pr = dti.getAsFileSystemHandle();
        return nkJSObject.RegisterObject(pr);
    }
};

window.nkDragEvent =
{
    DataTransfer: function (ev)
    {
        var dt = ev.dataTransfer;
        var dtid = nkJSObject.GetUid(dt);
        if (dtid !== -1)
            return dtid;
        return nkJSObject.RegisterObject(dt);
    }
};

window.nkFileSystemHandle =
{
    Kind: function (uid)
    {
        var fh = nkJSObject.GetObject(uid);
        switch (fh.kind)
        {
            case "file": return 1;
            case "directory": return 2;
            default: null;
        }
    },
    Name: function (uid)
    {
        var fh = nkJSObject.GetObject(uid);
        return fh.name;
    },
    IsSameEntry: function (uid, d)
    {
        var oid = Module.HEAP32[(d + 0) >> 2];
        var ha1 = nkJSObject.GetObject(uid);
        var ha2 = nkJSObject.GetObject(oid);
        var pr = ha1.isSameEntry(ha2);
        return nkJSObject.RegisterObject(pr);
    },
    QueryPermission: function (uid, d)
    {
        var ha = nkJSObject.GetObject(uid);
        let op = {};
        if (d)
        {
            var mo = nkJSObject.ReadString(d);
            if (mo) op.mode = mo;
        }
        var pr = ha.queryPermission(op);
        return nkJSObject.RegisterObject(pr);
    },
    RequestPermission: function (uid, d)
    {
        var ha = nkJSObject.GetObject(uid);
        let op = {};
        if (d)
        {
            var mo = nkJSObject.ReadString(d);
            if (mo) op.mode = mo;
        }
        var pr = ha.requestPermission(op);
        return nkJSObject.RegisterObject(pr);
    }
};

window.nkFileSystemFileHandle =
{
    GetFile: function (uid)
    {
        var fh = nkJSObject.GetObject(uid);
        var pr = fh.getFile();
        return nkJSObject.RegisterObject(pr);
    },
    CreateWritable: function (uid, d)
    {
        var fh = nkJSObject.GetObject(uid);
        var ke = Module.HEAP32[(d + 0) >> 2];
        var pr;
        if (ke)
        {
            let op = { keepExistingData: true };
            pr = fh.createWritable(op);
        }
        else
        {
            pr = fh.createWritable();
        }
        return nkJSObject.RegisterObject(pr);
    },
    CreateSyncAccessHandle: function (uid)
    {
        var fh = nkJSObject.GetObject(uid);
        var pr = fh.createSyncAccessHandle();
        return nkJSObject.RegisterObject(pr);
    }
};

window.nkFileSystemDirectoryHandle =
{
    GetEntries: function (uid)
    {
        var dh = nkJSObject.GetObject(uid);
        var pr = (async () =>
        {
            var en = [];
            for await (const [key, value] of dh.entries())
                en.push(value);
            return en;
        })();
        return nkJSObject.RegisterObject(pr);
    },
    GetFileHandle: function (uid, d)
    {
        var dh = nkJSObject.GetObject(uid);
        var na = nkJSObject.ReadString(d);
        var cr = Module.HEAP32[(d + 4) >> 2];
        var pr;
        if (cr)
        {
            let op = { create: true };
            pr = dh.getFileHandle(na, op);
        }
        else
        {
            pr = dh.getFileHandle(na);
        }
        return nkJSObject.RegisterObject(pr);
    },
    GetDirectoryHandle: function (uid, d)
    {
        var dh = nkJSObject.GetObject(uid);
        var na = nkJSObject.ReadString(d);
        var cr = Module.HEAP32[(d + 4) >> 2];
        var pr;
        if (cr)
        {
            let op = { create: true };
            pr = dh.getDirectoryHandle(na, op);
        }
        else
        {
            pr = dh.getDirectoryHandle(na);
        }
        return nkJSObject.RegisterObject(pr);
    },
    RemoveEntry: function (uid, d)
    {
        var dh = nkJSObject.GetObject(uid);
        var na = nkJSObject.ReadString(d);
        var re = Module.HEAP32[(d + 4) >> 2];
        var pr;
        if (re)
        {
            let op = { recursive: true };
            pr = dh.removeEntry(na, op);
        }
        else
        {
            pr = dh.removeEntry(na);
        }
        return nkJSObject.RegisterObject(pr);
    },
    Resolve: function (uid, d)
    {
        var dh = nkJSObject.GetObject(uid);
        var fhid = Module.HEAP32[(d + 0) >> 2];
        var fh = nkJSObject.GetObject(fhid);
        var pr = dh.resolve(fh).then((rpc) =>
        {
            var rp = "";
            for (var i = 0; i < rpc.length; i++)
            {
                if (i > 0) rp += "/";
                rp += rpc[i];
            }
            return rp;
        });
        return nkJSObject.RegisterObject(pr);
    }
};

window.nkFileSystemWritableFileStream =
{
    Locked: function (uid)
    {
        var wfs = nkJSObject.GetObject(uid);
        return wfs.locked;
    },
    WriteBytes: function (uid, d)
    {
        var wfs = nkJSObject.GetObject(uid);
        var pt = Module.HEAP32[(d + 0) >> 2];
        var len = Module.HEAP32[(d + 4) >> 2];
        var dv = new DataView(Module.HEAP32.buffer, pt, len);
        var pr = wfs.write(dv);
        return nkJSObject.RegisterObject(pr);
    },
    WriteBlob: function (uid, d)
    {
        var wfs = nkJSObject.GetObject(uid);
        var bid = Module.HEAP32[(d + 0) >> 2];
        var b = nkJSObject.GetObject(bid);
        var pr = wfs.write(b);
        return nkJSObject.RegisterObject(pr);
    },
    Seek: function (uid, d)
    {
        var wfs = nkJSObject.GetObject(uid);
        var po = Module.HEAP32[(d + 0) >> 2];
        var pr = wfs.seek(po);
        return nkJSObject.RegisterObject(pr);
    },
    Truncate: function (uid, d)
    {
        var wfs = nkJSObject.GetObject(uid);
        var si = Module.HEAP32[(d + 0) >> 2];
        var pr = wfs.truncate(si);
        return nkJSObject.RegisterObject(pr);
    },
    Abort: function (uid)
    {
        var wfs = nkJSObject.GetObject(uid);
        var pr = wfs.abort();
        return nkJSObject.RegisterObject(pr);
    },
    Close: function (uid)
    {
        var wfs = nkJSObject.GetObject(uid);
        var pr = wfs.close();
        return nkJSObject.RegisterObject(pr);
    }
};

window.nkFileSystemSyncAccessHandle =
{
    Read: function (uid, d)
    {
        var sah = nkJSObject.GetObject(uid);
        var bsid = Module.HEAP32[(d + 0) >> 2];
        var bs = nkJSObject.GetObject(bsid);
        var at = Module.HEAP32[(d + 4) >> 2];
        var pr;
        if (at)
        {
            var rwo = { at: BigInt(at) };
            pr = sah.read(bs, rwo);
        }
        else
        {
            sah.read(bs);
        }
        return nkJSObject.RegisterObject(pr);
    },
    Write: function (uid, d)
    {
        var sah = nkJSObject.GetObject(uid);
        var bsid = Module.HEAP32[(d + 0) >> 2];
        var bs = nkJSObject.GetObject(bsid);
        var at = Module.HEAP32[(d + 4) >> 2];
        var pr;
        if (at >= 0)
        {
            var rwo = { at: BigInt(at) };
            pr = sah.write(bs, rwo);
        }
        else
        {
            sah.write(bs);
        }
        return nkJSObject.RegisterObject(pr);
    },
    Truncate: function (uid, d)
    {
        var sah = nkJSObject.GetObject(uid);
        var ns = Module.HEAP32[(d + 0) >> 2];
        sah.truncate(ns);
    },
    GetSize: function (uid)
    {
        var sah = nkJSObject.GetObject(uid);
        return sah.getSize();
    },
    Flush: function (uid)
    {
        var sah = nkJSObject.GetObject(uid);
        sah.flush();
    },
    Close: function (uid)
    {
        var sah = nkJSObject.GetObject(uid);
        sah.close();
    }
};

window.nkFileSystemObserver =
{
    Create: function ()
    {
        var cb = (rs, ob) =>
        {
            for (var i = 0; i < rs.length; i++)
            {
                var cr = rs[i];
                var crid = nkJSObject.GetUid(cr);
                if (crid === -1)
                    crid = nkJSObject.RegisterObject(cr);
                DotNet.invokeMethod('nkast.Wasm.File', 'JsFileSystemObserverChangeRecord', crid, ob.nkUid);
            }
        };
        var fso = new FileSystemObserver(cb);
        return nkJSObject.RegisterObject(fso);
    },
    Observe: function (uid, d)
    {
        var fso = nkJSObject.GetObject(uid);
        var fhid = Module.HEAP32[(d + 0) >> 2];
        var fh = nkJSObject.GetObject(fhid);
        var re = Module.HEAP32[(d + 4) >> 2];
        var pr;
        if (re)
        {
            let op = { recursive: true };
            pr = fso.observe(fh, op);
        }
        else
        {
            pr = fso.observe(fh);
        }
        return nkJSObject.RegisterObject(pr);
    },
    Disconnect: function (uid)
    {
        var fso = nkJSObject.GetObject(uid);
        fso.disconnect();
    }
};

window.nkFileSystemChangeRecord =
{
    ChangedHandle: function (uid)
    {
        var cr = nkJSObject.GetObject(uid);
        var ch = cr.changedHandle;
        if (ch)
        {
            var chid = nkJSObject.GetUid(ch);
            if (chid !== -1)
                return chid;
            return nkJSObject.RegisterObject(ch);
        }
        else
        {
            return -1;
        }
    },
    RelativePathComponents: function (uid)
    {
        var cr = nkJSObject.GetObject(uid);
        var rpc = cr.relativePathComponents;
        var rp = "";
        for (var i = 0; i < rpc.length; i++)
        {
            if (i > 0) rp += "/";
            rp += rpc[i];
        }
        return rp;
    },
    RelativePathMovedFrom: function (uid)
    {
        var cr = nkJSObject.GetObject(uid);
        var rpc = cr.relativePathMovedFrom;
        var rp = "";
        for (var i = 0; i < rpc.length; i++)
        {
            if (i > 0) rp += "/";
            rp += rpc[i];
        }
        return rp;
    },
    Root: function (uid)
    {
        var cr = nkJSObject.GetObject(uid);
        var r = cr.root;
        var rid = nkJSObject.GetUid(r);
        if (rid !== -1)
            return rid;
        return nkJSObject.RegisterObject(r);
    },
    Type: function (uid)
    {
        var cr = nkJSObject.GetObject(uid);
        switch (cr.type)
        {
            case "appeared" : return 1;
            case "disappeared": return 2;
            case "errored": return 3;
            case "modified": return 4;
            case "moved": return 5;
            case "unknown": return 6;
            default: return null;
        }
    }
};

window.nkFileSystemAccess =
{
    EnumFileSystemPermissionMode: function (i)
    {
        switch (i)
        {
            case 1: return "read";
            case 2: return "readwrite";
        }
    },
    EnumWellKnownDirectory: function (d)
    {
        var sie = Module.HEAP32[d >> 2];
        switch (sie)
        {
            case 1: return "desktop";
            case 2: return "documents";
            case 3: return "downloads";
            case 4: return "music";
            case 5: return "pictures";
            case 6: return "videos";
            default: return null;
        }
    },
    ObjectFilePickerFilter: function (d)
    {
        var mt = nkJSObject.ReadString(d);
        var arr = Module.HEAP32[(d + 4) >> 2];
        var arrPtr = Blazor.platform.getArrayEntryPtr(arr, 0, 4);
        var arrLen = Blazor.platform.getArrayLength(arr);
        var exs = [];
        for (var i = 0; i < arrLen; i++)
        {
            var t = nkJSObject.ReadString(arrPtr + (i * 4));
            exs.push(t);
        }
        var f =
        {
            mimeType: mt,
            extensions: exs
        };
        return f;
    },
    ObjectFilePickerAcceptType: function (d)
    {
        var de = nkJSObject.ReadString(d);
        var arr = Module.HEAP32[(d + 4) >> 2];
        var arrPtr = Blazor.platform.getArrayEntryPtr(arr, 0, 4);
        var arrLen = Blazor.platform.getArrayLength(arr);
        var fs = {};
        for (var i = 0; i < arrLen; i++)
        {
            var f = this.ObjectFilePickerFilter(arrPtr + (i * 4));
            fs[f.mimeType] = f.extensions
        }
        var at =
        {
            description: de,
            accept: fs
        };
        return at;
    },
    ObjectFilePickerAcceptTypeArray: function (d)
    {
        var arr = Module.HEAP32[d >> 2];
        var arrPtr = Blazor.platform.getArrayEntryPtr(arr, 0, 4);
        var arrLen = Blazor.platform.getArrayLength(arr);
        var ats = [];
        for (var i = 0; i < arrLen; i++)
        {
            var at = this.ObjectFilePickerAcceptType(arrPtr + (i * 4));
            ats.push(at);
        }
        return ats;
    },
    ObjectFilePickerStartIn: function (d)
    {
        var sip = Module.HEAP32[d >> 2];
        if (sip)
        {
            return nkJSObject.GetObject(sip);
        }
        else
        {
            var sie = this.EnumWellKnownDirectory(d + 4);
            return sie;
        }
    }
};

window.nkStorageManager =
{
    Create: function (uid)
    {
        var nv = nkJSObject.GetObject(uid);
        var sm = nv.storage;
        var uid = nkJSObject.GetUid(sm);
        if (uid !== -1)
            return uid;
        return nkJSObject.RegisterObject(sm);
    },
    Persisted: function (uid)
    {
        var sm = nkJSObject.GetObject(uid);
        var pr = sm.persisted();
        return nkJSObject.RegisterObject(pr);
    },
    Persist: function (uid)
    {
        var sm = nkJSObject.GetObject(uid);
        var pr = sm.persist();
        return nkJSObject.RegisterObject(pr);
    },
    Estimate: function (uid, d)
    {
        var sm = nkJSObject.GetObject(uid);
        var pr = sm.estimate();
        return nkJSObject.RegisterObject(pr);
    },
    EstimateUsage: function (d)
    {
        var es = nkJSObject.GetObject(uid);
        return es.usage;
    },
    EstimateQuota: function (d)
    {
        var es = nkJSObject.GetObject(uid);
        return es.quota;
    },
    GetDirectory: function (uid)
    {
        var sm = nkJSObject.GetObject(uid);
        var pr = sm.getDirectory();
        return nkJSObject.RegisterObject(pr);
    }
};

window.nkUrl =
{
    CreateObjectURL: function (uid)
    {
        var obj = nkJSObject.GetObject(uid);
        var url = URL.createObjectURL(obj);
        return url;
    },
    RevokeObjectURL: function (uid)
    {
        var url = nkJSObject.GetObject(uid);
        if (url)
            URL.revokeObjectURL(url);
    },
    DownloadFromURL: function (d)
    {
        var url = nkJSObject.ReadString(d);
        var na = nkJSObject.ReadString(d + 4);
        var an = document.createElement("a");
        an.href = url;
        an.download = na;
        document.body.appendChild(an);
        an.click();
        document.body.removeChild(an);
    }
};
