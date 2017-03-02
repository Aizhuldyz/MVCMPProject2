using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebServer
{
    public class Constants
    {
        internal static readonly string[] IndexFiles = {
        "index.html",
        "index.htm",
        "default.html",
        "default.htm"
        };

        internal static readonly Dictionary<string, bool> FilteredExtensions = new Dictionary<string, bool>()
        {
            {".cs", true},
            {".config", true}
        };
    }
}
