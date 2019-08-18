using System;

namespace MyCommunity.Utility.Extensions
{
    public static class CustomId
    {
        public static Guid ToGuidId(this string id)
        {
            try
            {
                // replace Square shape in UTF8 with ascii dash (-) see 
                id = id.Replace((char)8208, (char)45);
                id = id.Replace("-", String.Empty).ToUpper();
                ulong lCustomerId = Base36.Decode(id);
                byte[] bitConvertedBytes = BitConverter.GetBytes(lCustomerId);
                byte[] guidBytes = new byte[16];
                bitConvertedBytes.CopyTo(guidBytes, 8);

                Guid resultGuid = new Guid(guidBytes);
                return resultGuid;
            }
            catch (Exception)
            {
                return Guid.Empty;
            }
        }

        public static string ToFriendlyId(this Guid guid)
        {
            if (guid == Guid.Empty)
                return "No GUID supplied for friendly ID";

            string str = Base36.Encode(BitConverter.ToUInt64(guid.ToByteArray(), 8));
            if (string.IsNullOrEmpty(str))
                return "Problem getting friendly name from GUID";
            string part3 = str.Length > 8
                ? str.Substring(8, str.Length - 8).PadRight(4, '0')
                : 0.ToString().PadRight(4, '0');

            string part2 = str.Length > 4
                ? str.Length < 8
                    ? str.Substring(4, str.Length - 4).PadRight(4, '0')
                    : str.Substring(4, 4)
                : 0.ToString().PadRight(4, '0');

            string part1 = str.Length < 4
                ? str.Substring(0, str.Length).PadRight(4, '0')
                : str.Substring(0, 4);

            string resultID = string.Format("{0}-{1}-{2}", part1, part2, part3);
            return resultID;
        }

        public static string ToFriendlyId(this string guid)
        {
            if (string.IsNullOrEmpty(guid))
                return new Guid().ToFriendlyId();
            return new Guid(guid).ToFriendlyId();
        }

        public static bool IsFriendlyId(this string id)
        {
            // support customer id 2HMJ-IM2T-UZ53F for example
            return (id.Length == 14 || id.Length == 15) && id[4] == '-' && id[9] == '-';
        }
    }
}
