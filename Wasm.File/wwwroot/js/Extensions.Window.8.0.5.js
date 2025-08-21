Object.assign(window.nkWindow,
{
    ShowDirectoryPicker: function (uid, d)
    {
        var w = nkJSObject.GetObject(uid);
        var o = Module.HEAP32[d >> 2];
        var pr;
        if (o)
        {
            var id = nkJSObject.ReadString(d + 4);
            var mo = window.nkFileSystemAccess.EnumFileSystemPermissionMode(Module.HEAP32[(d + 8) >> 2]);
            var si = window.nkFileSystemAccess.ObjectFilePickerStartIn(d + 12);

            let op = {};
            if (id) { op.id = id; }
            if (mo) { op.mode = mo; }
            if (si) { op.startIn = si; }
            pr = w.showDirectoryPicker(op);
        }
        else
        {
            pr = w.showDirectoryPicker();
        }
        return nkJSObject.RegisterObject(pr);
    },
    ShowOpenFilePicker: function (uid, d)
    {
        var w = nkJSObject.GetObject(uid);
        var o = Module.HEAP32[d >> 2];
        var pr;
        if (o)
        {
            var ty = window.nkFileSystemAccess.ObjectFilePickerAcceptTypeArray(d + 4);
            var ex = Module.HEAP32[(d + 8) >> 2];
            var id = nkJSObject.ReadString(d + 12);
            var si = window.nkFileSystemAccess.ObjectFilePickerStartIn(d + 16);
            var mu = Module.HEAP32[(d + 24) >> 2];

            let op = {};
            if (ty) { op.types = ty }
            if (ex) { op.excludeAcceptAllOption = true; }
            if (id) { op.id = id; }
            if (si) { op.startIn = si; }
            if (mu) { op.multiple = true; }
            pr = w.showOpenFilePicker(op);
        }
        else
        {
            pr = w.showOpenFilePicker();
        }
        return nkJSObject.RegisterObject(pr);
    },
    ShowSaveFilePicker: function (uid, d)
    {
        var w = nkJSObject.GetObject(uid);
        var o = Module.HEAP32[d >> 2];
        var pr;
        if (o)
        {
            var ty = window.nkFileSystemAccess.ObjectFilePickerAcceptTypeArray(d + 4);
            var ex = Module.HEAP32[(d + 8) >> 2];
            var id = nkJSObject.ReadString(d + 12);
            var si = window.nkFileSystemAccess.ObjectFilePickerStartIn(d + 16);
            var sn = nkJSObject.ReadString(d + 24);

            let op = {};
            if (ty) { op.types = ty }
            if (ex) { op.excludeAcceptAllOption = true; }
            if (id) { op.id = id; }
            if (si) { op.startIn = si; }
            if (sn) { op.suggestedName = sn; }
            pr = w.showSaveFilePicker(op);
        }
        else
        {
            pr = w.showSaveFilePicker();
        }
        return nkJSObject.RegisterObject(pr);
    }
});
