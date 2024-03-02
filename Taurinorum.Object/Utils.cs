using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security;
using System.Text;

namespace Taurinorum.Object
{
    public static class Utils
    {
        /// <summary>
        /// Il metodo crea il campo Xml normalizzato per il dato passato in Input
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string CreateXmlTag(string nomeTag, string campo)
        {
            string campoXml = string.Empty;
            if (campo != null)
            {
                string campoNormalizzato = SecurityElement.Escape(campo); // campo.Replace("<", "&lt;").Replace(">", "&gt;").Replace("&", "&amp;");
                campoXml = string.Format("<{0}>{1}</{0}>", nomeTag, campoNormalizzato.Trim());
            }
            return campoXml;
        }
        public static string CreateXmlTag(string nomeTag, int campo)
        {
            return CreateXmlTag(nomeTag, campo.ToString());
        }
        public static string CreateXmlTag(string nomeTag, int? campo)
        {
            return CreateXmlTag(nomeTag, (campo == null) ? "0" : campo.ToString());
        }
        public static int ConvertiInt(bool valore)
        {
            if (valore == true)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public static string ConvertDateTime(DateTime Date)
        {
            if (Date != null && Date != new DateTime())
            {
                return Date.ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                return "";
            }
        }
        public static string ConvertDate(DateTime Date)
        {
            if (Date != null && Date != new DateTime())
            {
                return Date.ToString("yyyy-MM-dd");
            }
            else
            {
                return "";
            }
        }
        public static string ConvertTime(DateTime Date)
        {
            if (Date != null && Date != new DateTime())
            {
                return Date.ToString("HH:mm");
            }
            else
            {
                return "";
            }
        }
        public static bool ConvertiBool(int valore)
        {
            if (valore == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static string ConvertiBoolToIntString(bool valore)
        {
            if (valore)
                return "1";
            else
                return "0";
        }
        internal static string ConvertiDecimal(decimal value)
        {
            CultureInfo culture = new CultureInfo("en-US");
            //decimal valueStandardFormat = Convert.ToDecimal(value, culture);
            return string.Format(culture, "{0:0.#####}", value);

        }
    }
}
