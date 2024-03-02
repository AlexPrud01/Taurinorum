using System;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace Taurinorum.Object
{
    public class Shared
    {

        #region << CONVERT TO XML >>
        public static string ConvertStringToXML(string valore)
        {
            return valore;
        }
        public static string ConvertDataToXML(DateTime dt)
        {
            return dt.ToString("yyyy-M-dd");
        }
        public static string ConvertDecimalToXML(decimal valore)
        {
            return valore.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
        }
        #endregion

        public static string GetXmlNormalizedString(string valore)
        {
            if (!string.IsNullOrEmpty(valore))
            {
                return System.Net.WebUtility.HtmlDecode(valore);
                //return valore.Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
            }
            return "";
        }
        public static bool GetBoolFromString(string valore)
        {
            bool returnValue = false;

            if (valore == "1")
                returnValue = true;

            if (valore == "-1")
                returnValue = true;

            return returnValue;
        }
        public static int GetIntFromString(string valore)
        {
            int returnValue = 0;

            if (!string.IsNullOrEmpty(valore) && !string.IsNullOrWhiteSpace(valore))
            {
                try
                {
                    _ = int.TryParse(valore, out returnValue);
                }
                catch (Exception e)
                {
                    Console.Write(e.Message);
                }
            }

            return returnValue;
        }
        public static short GetShortFromString(string valore)
        {
            short returnValue = 0;

            if (!string.IsNullOrEmpty(valore) && !string.IsNullOrWhiteSpace(valore))
            {
                try
                {
                    _ = short.TryParse(valore, out returnValue);
                }
                catch (Exception e)
                {
                    Console.Write(e.Message);
                }
            }

            return returnValue;
        }

        public static ushort GetUShortFromString(string valore)
        {
            ushort returnValue = 0;

            if (!string.IsNullOrEmpty(valore) && !string.IsNullOrWhiteSpace(valore))
            {
                try
                {
                    _ = ushort.TryParse(valore, out returnValue);
                }
                catch (Exception e)
                {
                    Console.Write(e.Message);
                }
            }

            return returnValue;
        }
        public static DateTime GetDateTimeFromString(string valore)
        {
            DateTime returnValue = DateTime.MinValue;

            if (!string.IsNullOrEmpty(valore) && !string.IsNullOrWhiteSpace(valore))
            {
                try
                {
                    _ = DateTime.TryParse(valore, out returnValue);
                }
                catch (Exception e)
                {
                    Console.Write(e.Message);
                }
            }

            return returnValue;
        }
        public static decimal GetDecimalFromString(string valore)
        {
            decimal returnValue = 0;

            if (!string.IsNullOrEmpty(valore) && !string.IsNullOrWhiteSpace(valore))
            {
                try
                {
                    _ = decimal.TryParse(valore, out returnValue);
                }
                catch (Exception e)
                {
                    Console.Write(e.Message);
                }
            }

            return returnValue;
        }
        public static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using SHA256 sha256Hash = SHA256.Create();
            // ComputeHash - returns byte array  
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

            // Convert byte array to a string   
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}
