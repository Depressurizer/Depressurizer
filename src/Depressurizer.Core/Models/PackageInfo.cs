using Depressurizer.Core.Enums;
using Depressurizer.Core.Helpers;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using ValveKeyValue;

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

        private const uint Magic27 = 0x06_56_55_27;
        private const uint Magic28 = 0x06_56_55_28;

        /// <summary>
        ///     Loads Apps from packageinfo.vdf.
        ///     See https://github.com/SteamDatabase/SteamAppInfo#packageinfovdf
        /// </summary>
        /// <param name="path">Path of packageinfo.vdf</param>
        public static Dictionary<int, PackageInfo> LoadPackages(string path)
        {
            Dictionary<int, PackageInfo> result = new Dictionary<int, PackageInfo>();

            try
            {
                var _fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                var reader = new BinaryReader(_fileStream);

                // Read some header fields
                var magic = reader.ReadUInt32();
                if (magic != Magic27 && magic != Magic28)
                {
                    throw new InvalidDataException("Invalid VDF format");
                }

                EUniverse Universe = (EUniverse)reader.ReadUInt32();

                var options = new KVSerializerOptions();
                var deserializer = KVSerializer.Create(KVSerializationFormat.KeyValues1Binary);

                while (true)
                {
                    var appid = reader.ReadUInt32();

                    if (appid == 0xFFFFFFFF)
                    {
                        break;
                    }

                    var hash = new ReadOnlyCollection<byte>(reader.ReadBytes(20));
                    var changeNumber = reader.ReadUInt32();

                    if (magic == Magic28)
                    {
                        var token = reader.ReadUInt64();
                    }

                    PackageInfo package = ReadEntries(deserializer.Deserialize(_fileStream, options));
                    result.Add(package.Id, package);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                throw;
            }

            return result;
        }

        private static PackageInfo ReadEntries(KVObject data)
        {
            PackageInfo package = new PackageInfo((int)data["packageid"]);
            package.BillingType = (PackageBillingType)(int)data["billingtype"];
            foreach(var id in (IEnumerable<KVObject>)data["appids"])
            {
                package.AppIds.Add((int)id.Value);
            }

            return package;
        }

        #endregion
    }
}
