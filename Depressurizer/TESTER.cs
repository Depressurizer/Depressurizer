using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Depressurizer {
    class TESTER {
        static void Main() {
            string path = @"D:\Steam\appcache\packageinfo.vdf";
            PackageInfoCollection pic = new PackageInfoCollection();
            pic.LoadFromFile( path );
        }
    }
}
