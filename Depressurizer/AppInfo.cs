using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Depressurizer {
    class AppInfo {
        int appId;
        string name;
        string type;
    }

    class AppInfoList {
        Dictionary<uint, AppInfo> appList;

        public AppInfoList() {
            appList = new Dictionary<uint, AppInfo>();
        }

        public void LoadFromFile( string path ) {
            FileStream fStream = File.Open( path, FileMode.Open );

            BinaryReader bReader = new BinaryReader( fStream );

            // seek to real start

            do {
                int last = bReader.ReadByte();
                int next = bReader.PeekChar();

                // if next is \00 and last isn't, new section
                // exception for inside sections, like depots. then, if last is \00, new section

                // if last is \08 and next is \08, end section

                // if last is \00, end entry

                // if last is \01 or \02, new value

                // if next or last is -1, end file
            } while( true );
        }
    }
}
