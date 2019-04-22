using System.Collections.Generic;
using System.IO;
using System.Text;
using Depressurizer.Core.Enums;
using JetBrains.Annotations;

namespace Depressurizer.Core.Models
{
    public class PackageInfo
    {
        #region Fields

        private List<int> _appIds;

        #endregion

        #region Constructors and Destructors

        public PackageInfo(int id = 0)
        {
            Id = id;
        }

        #endregion

        #region Public Properties

        [NotNull]
        public List<int> AppIds
        {
            get => _appIds ?? (_appIds = new List<int>());
            set => _appIds = value;
        }

        public PackageBillingType BillingType { get; set; }

        public int Id { get; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Loads Apps from packageinfo.vdf.
        /// </summary>
        /// <param name="path">Path of packageinfo.vdf</param>
        public static Dictionary<int, PackageInfo> LoadPackages(string path)
        {
            Dictionary<int, PackageInfo> result = new Dictionary<int, PackageInfo>();

            /* packageinfo.vdf entry example format, sometimes has extra values. Line breaks are only for readability and not part of format.
            * we only care about *packageid*, *billingtype*, *appids*
            * *undeciphered*(24 bytes i haven't deciphered) *changenumber*(4 bytes, little endian) 
            * 00 *packageid*(variable size, big endian, ascii) 00
            * 02 packageid 00 *packageid*(4 bytes, little endian) 
            * 02 billingtype 00 *billingtype*(4 bytes, little endian) 
            * 02 licensetype 00 *licensetype*(4 bytes, little endian) 
            * 02 status 00 00 00 00 00 00 extended 00 
            * 08 00 appids 00 02 *entrynumber*(variable size, number stored as string(ascii), starts at 0, random example: 31 38 39=189) 00 *appid*(4 bytes, little endian) 
            * 08 00 depotids 00 02 *entrynumber* 00 *depotid*(4 bytes, little endian) 02 *entrynumber* 00 *depotid* 02 *entrynumber* 00 *depotid* 
            * 08 00 appitems 00 08 08 08 
            */

            BinaryReader bReader = new BinaryReader(new FileStream(path, FileMode.Open), Encoding.ASCII);
            long fileLength = bReader.BaseStream.Length;

            // seek to packageid: start of a new entry
            byte[] packageidBytes =
            {
                0x00,
                0x02,
                0x70,
                0x61,
                0x63,
                0x6B,
                0x61,
                0x67,
                0x65,
                0x69,
                0x64,
                0x00
            }; // 0x00 0x02 p a c k a g e i d 0x00
            byte[] billingtypeBytes =
            {
                0x02,
                0x62,
                0x69,
                0x6C,
                0x6C,
                0x69,
                0x6E,
                0x67,
                0x74,
                0x79,
                0x70,
                0x65,
                0x00
            }; // 0x02 b i l l i n g t y p e 0x00
            byte[] appidsBytes =
            {
                0x08,
                0x00,
                0x61,
                0x70,
                0x70,
                0x69,
                0x64,
                0x73,
                0x00
            }; // 0x08 0x00 appids 0x00

            VDFNode.ReadBin_SeekTo(bReader, packageidBytes, fileLength);
            while (bReader.BaseStream.Position < fileLength)
            {
                int id = bReader.ReadInt32();
                PackageInfo package = new PackageInfo(id);

                VDFNode.ReadBin_SeekTo(bReader, billingtypeBytes, fileLength);
                package.BillingType = (PackageBillingType) bReader.ReadInt32();

                VDFNode.ReadBin_SeekTo(bReader, appidsBytes, fileLength);
                while (bReader.ReadByte() == 0x02)
                {
                    while (bReader.ReadByte() != 0x00) { }

                    package.AppIds.Add(bReader.ReadInt32());
                }

                result.Add(package.Id, package);
                VDFNode.ReadBin_SeekTo(bReader, packageidBytes, fileLength);
            }

            return result;
        }

        #endregion
    }
}
