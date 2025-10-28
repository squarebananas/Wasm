Object.assign(window.nkHTMLElement,
{
    RegisterExtensionDragAndDrop: function (uid)
    {
        var he = nkJSObject.GetObject(uid);
        if (he.dragdropextension)
            return nkJSObject.GetUid(he.dragdropextension);
        var dde = { attachedToUid: uid };
        return nkJSObject.RegisterObject(dde);
    },
    RegisterEventsDragDrop: function (uid)
    {
        var dde = nkJSObject.GetObject(uid);
        var heid = dde.attachedToUid;
        var he = nkJSObject.GetObject(heid);
        he.addEventListener('dragstart', (event) =>
        {
            event.preventDefault();
            var dtid = nkDragEvent.DataTransfer(event);
            DotNet.invokeMethod('nkast.Wasm.File', 'JsHTMLElementOnDragStart', uid, dtid);
        });
        he.addEventListener('drag', (event) =>
        {
            event.preventDefault();
            var dtid = nkDragEvent.DataTransfer(event);
            DotNet.invokeMethod('nkast.Wasm.File', 'JsHTMLElementOnDrag', uid, dtid);
        });
        he.addEventListener('dragenter', (event) =>
        {
            event.preventDefault();
            var dtid = nkDragEvent.DataTransfer(event);
            DotNet.invokeMethod('nkast.Wasm.File', 'JsHTMLElementOnDragEnter', uid, dtid);
        });
        he.addEventListener('dragleave', (event) =>
        {
            event.preventDefault();
            var dtid = nkDragEvent.DataTransfer(event);
            DotNet.invokeMethod('nkast.Wasm.File', 'JsHTMLElementOnDragLeave', uid, dtid);
        });
        he.addEventListener('dragover', (event) =>
        {
            event.preventDefault();
            var dtid = nkDragEvent.DataTransfer(event);
            DotNet.invokeMethod('nkast.Wasm.File', 'JsHTMLElementOnDragOver', uid, dtid);
        });
        he.addEventListener('drop', (event) =>
        {
            event.preventDefault();
            var dtid = nkDragEvent.DataTransfer(event);
            DotNet.invokeMethod('nkast.Wasm.File', 'JsHTMLElementOnDrop', uid, dtid);
        });
        he.addEventListener('dragend', (event) =>
        {
            event.preventDefault();
            var dtid = nkDragEvent.DataTransfer(event);
            DotNet.invokeMethod('nkast.Wasm.File', 'JsHTMLElementOnDragEnd', uid, dtid);
        });
    },
    UnregisterEventsDragDrop: function (uid)
    {
        var dde = nkJSObject.GetObject(uid);
        var he = dde.attachedToUid;
        de.dragstart = null;
        de.drag = null;
        de.dragenter = null;
        de.dragleave = null;
        de.dragover = null;
        he.drop = null;
        de.dragend = null;
    }
});
