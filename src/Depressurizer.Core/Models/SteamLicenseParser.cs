using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;

/*
 * Thanks a lot to:
 * - https://github.com/wynick27/steam-missing-covers-downloader/blob/master/license_parser.py for the decryptiong logic on how to read the Steam licensecache files
 * - https://github.com/SteamDatabase/Protobufs/blob/master/steam/steammessages_clientserver.proto#L161 for the Protobuf definition
 * 
 * This code wouldn't have been possible without you!
 */

namespace Depressurizer.Core.Models
{
    #region Protobuf Class
    [ProtoContract]
    public class CMsgClientLicenseList
    {
        [ProtoMember(1, IsRequired = false)]
        public int EResult { get; set; } = 2; // Default = 2

        [ProtoMember(2)]
        public List<License> Licenses { get; set; } = [];

        [ProtoContract]
        public class License
        {
            [ProtoMember(1, IsRequired = false)]
            public uint PackageId { get; set; }

            [ProtoMember(2, IsRequired = false)]
            public uint TimeCreated { get; set; }

            [ProtoMember(3, IsRequired = false)]
            public uint TimeNextProcess { get; set; }

            [ProtoMember(4, IsRequired = false)]
            public int MinuteLimit { get; set; }

            [ProtoMember(5, IsRequired = false)]
            public int MinutesUsed { get; set; }

            [ProtoMember(6, IsRequired = false)]
            public uint PaymentMethod { get; set; }

            [ProtoMember(7, IsRequired = false)]
            public uint Flags { get; set; }

            [ProtoMember(8, IsRequired = false)]
            public string? PurchaseCountryCode { get; set; }

            [ProtoMember(9, IsRequired = false)]
            public uint LicenseType { get; set; }

            [ProtoMember(10, IsRequired = false)]
            public int TerritoryCode { get; set; }

            [ProtoMember(11, IsRequired = false)]
            public int ChangeNumber { get; set; }

            [ProtoMember(12, IsRequired = false)]
            public uint OwnerId { get; set; }

            [ProtoMember(13, IsRequired = false)]
            public uint InitialPeriod { get; set; }

            [ProtoMember(14, IsRequired = false)]
            public uint InitialTimeUnit { get; set; }

            [ProtoMember(15, IsRequired = false)]
            public uint RenewalPeriod { get; set; }

            [ProtoMember(16, IsRequired = false)]
            public uint RenewalTimeUnit { get; set; }

            [ProtoMember(17, IsRequired = false)]
            public ulong AccessToken { get; set; }

            [ProtoMember(18, IsRequired = false)]
            public uint MasterPackageId { get; set; }
        }
    }
    #endregion

    public class RandomStream
    {
        private const uint MAX_RANDOM_RANGE = 0x7FFFFFFF;
        private const int NTAB = 32;
        private const int IA = 16807;
        private const int IM = 2147483647;
        private const int IQ = 127773;
        private const int IR = 2836;
        private const int NDIV = 1 + (IM - 1) / NTAB;

        private int m_idum;
        private int m_iy;
        private readonly int[] m_iv = new int[NTAB];

        public void SetSeed(int iSeed)
        {
            m_idum = (iSeed < 0) ? iSeed : -iSeed;
            m_iy = 0;
            for (int i = 0; i < NTAB; i++)
            {
                m_iv[i] = 0;
            }
        }

        public int GenerateRandomNumber()
        {
            int j, k;

            if (m_idum <= 0 || m_iy == 0)
            {
                if (-m_idum < 1)
                    m_idum = 1;
                else
                    m_idum = -m_idum;

                for (j = NTAB + 7; j >= 0; j--)
                {
                    k = m_idum / IQ;
                    m_idum = IA * (m_idum - k * IQ) - IR * k;
                    if (m_idum < 0)
                        m_idum += IM;
                    if (j < NTAB)
                        m_iv[j] = m_idum;
                }
                m_iy = m_iv[0];
            }

            k = m_idum / IQ;
            m_idum = IA * (m_idum - k * IQ) - IR * k;
            if (m_idum < 0)
                m_idum += IM;

            j = m_iy / NDIV;
            if (j >= NTAB || j < 0)
                j = (j % NTAB) & 0x7fffffff;

            m_iy = m_iv[j];
            m_iv[j] = m_idum;

            return m_iy;
        }

        public int RandomInt(int iLow, int iHigh)
        {
            int x = iHigh - iLow + 1;
            if (x <= 1 || MAX_RANDOM_RANGE < x - 1)
                return iLow;

            uint maxAcceptable = MAX_RANDOM_RANGE - ((MAX_RANDOM_RANGE + 1U) % (uint)x);
            int n;
            do
            {
                n = GenerateRandomNumber();
            } while (n > maxAcceptable);

            return iLow + (n % x);
        }

        public byte RandomChar()
        {
            return (byte)RandomInt(32, 126);
        }

        public byte[] DecryptData(int key, byte[] data)
        {
            SetSeed(key);
            byte[] result = new byte[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                byte randByte = RandomChar();
                result[i] = (byte)(data[i] ^ randByte);
            }
            return result;
        }
    }

    public static class LicenseParser
    {
        public static CMsgClientLicenseList Parse(string path, int steamId)
        {
            byte[] encrypted = File.ReadAllBytes(path);

            RandomStream random = new();
            byte[] decrypted = random.DecryptData(steamId, encrypted);

            var msg = Serializer.Deserialize<CMsgClientLicenseList>(
                new ReadOnlySpan<byte>(decrypted, 0, decrypted.Length - 4)
            );
            return msg;
        }
    }
}
