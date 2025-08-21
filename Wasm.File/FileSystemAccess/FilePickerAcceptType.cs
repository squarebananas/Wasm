using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nkast.Wasm.FileSystemAccess
{
    public struct FilePickerAcceptType
    {
        public struct Filter
        {
            public string MIMEType;
            public string[] Extensions;
        }

        public string Description;
        public Filter[] Filters;
    }
}
