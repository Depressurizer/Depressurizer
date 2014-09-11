using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.IO;

namespace Depressurizer {
    

    class PackageInfo {

        public enum ParseLocation {
            None, AppIDs, DepotIDs, Extended
        }

        public StringDictionary extended;
        public Dictionary<uint, uint> appIds;
        public Dictionary<uint, uint> depotIds;
        public uint packageId;
        public string name;

        public PackageInfo() {
            extended = new StringDictionary();
            appIds = new Dictionary<uint, uint>();
            depotIds = new Dictionary<uint, uint>();
            packageId = 0;
            name = null;
        }

        public bool AddValue( string key, string value, ParseLocation loc ) {
            if( loc == ParseLocation.AppIDs ) {
                uint k, v;
                if( !uint.TryParse( key, out k ) ) return false;
                if( !uint.TryParse( value, out v ) ) return false;
                appIds.Add( k, v );
                return true;
            } else if( loc == ParseLocation.DepotIDs ) {
                uint k, v;
                if( !uint.TryParse( key, out k ) ) return false;
                if( !uint.TryParse( value, out v ) ) return false;
                depotIds.Add( k, v );
                return true;
            } else {
                if( key == "name" ) {
                    name = value;
                    return true;
                } else if( key == "PackageID" ) {
                    uint v;
                    if( !uint.TryParse( value, out v ) ) return false;
                    packageId = v;
                    return true;
                } else {
                    extended.Add( key, value );
                }
            }
            return true;
        }

        public bool AddValue( string key, uint value, ParseLocation loc ) {
            if( loc == ParseLocation.AppIDs ) {
                uint k;
                if( !uint.TryParse( key, out k ) ) return false;
                appIds.Add( k, value );
                return true;
            } else if( loc == ParseLocation.DepotIDs ) {
                uint k;
                if( !uint.TryParse( key, out k ) ) return false;
                depotIds.Add( k, value );
                return true;
            } else {
                if( key == "name" ) {
                    // This is stupid, but I'll leave it
                    name = value.ToString();
                    return true;
                } else if( key == "PackageID" ) {
                    packageId = value;
                    return true;
                } else {
                    extended.Add( key, value.ToString() );
                }
            }
            return true;
        }

    }

    class PackageInfoCollection {
        public Dictionary<uint, PackageInfo> packages;

        public PackageInfoCollection() {
            packages = new Dictionary<uint, PackageInfo>();
        }

        public void LoadFromFile( string file ) {
            FileStream fStream = File.Open( file, FileMode.Open );

            BinaryReader bReader = new BinaryReader( fStream );

            PackageInfo.ParseLocation currentLoc = PackageInfo.ParseLocation.None;
            PackageInfo package = new PackageInfo();

            fStream.Seek( 38, SeekOrigin.Begin );

            do {
                int next = (byte)bReader.ReadChar();
                if( next == 0x00 ) { // indicates a section start
                    string token = RallionIO.ReadStringToNull( fStream );
                    if( token == "appids" ) {
                        currentLoc = PackageInfo.ParseLocation.AppIDs;
                    } else if( token == "depotids" ) {
                        currentLoc = PackageInfo.ParseLocation.DepotIDs;
                    } else if( token == "extended" ) {
                        currentLoc = PackageInfo.ParseLocation.Extended;
                    } else {
                        // shouldn't be here!
                    }
                } else if( next == 0x01 ) { // new key / text value
                    string key = RallionIO.ReadStringToNull( fStream );
                    string val = RallionIO.ReadStringToNull( fStream );
                    package.AddValue( key, val, currentLoc );
                } else if( next == 0x02 ) { // new key / 4-byte int value
                    string key = RallionIO.ReadStringToNull( fStream );
                    uint val = bReader.ReadUInt32();
                    package.AddValue( key, val, currentLoc );
                } else if( next == 0x08 ) { // end current section. the hard part.
                    if( currentLoc != PackageInfo.ParseLocation.None ) {
                        currentLoc = PackageInfo.ParseLocation.None;
                    } else {
                        if( package.packageId != 0 ) {
                            packages.Add( package.packageId, package );
                        }
                        bReader.ReadBytes( 31 );
                        int n = bReader.PeekChar();

                        if( n == -1 ) return;

                        package = new PackageInfo();
                    }
                } else {
                    // unknown char, so I guess just do nothing
                }



            } while( true );
        }
    }

    public static class RallionIO {
        public static string ReadStringToNull( Stream s, bool consumeNull = true ) {
            StringBuilder builder = new StringBuilder();
            int next = s.ReadByte();
            while( next != 0 && next != -1 ) {
                builder.Append( (char)next );
                next = s.ReadByte();
            }
            if( next == 0 && !consumeNull ) {
                s.Seek( -1, SeekOrigin.Current );
            }
            return builder.ToString();
        }
    }
}
