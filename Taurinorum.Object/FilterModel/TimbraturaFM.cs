using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Taurinorum.Object.FilterModel
{
    public class TimbraturaFM : BaseFM
    {
        public int IDUtente { get; set; } = 0;
        public decimal Longitudine { get; set; } = 0;
        public decimal Latitudine { get; set; } = 0;
        public bool EntUsc { get; set; }
        public DateTime DataOra { get; set; }
        public DateTime DaData { get; set; } = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        public DateTime AData { get; set; } = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));

        public override string ToXml()
        {
            string xml = "<params>";

            xml += Utils.CreateXmlTag("IDUtente", Utils.ConvertiDecimal(IDUtente));
            xml += Utils.CreateXmlTag("Longitudine", Longitudine.ToString());
            xml += Utils.CreateXmlTag("Latitudine", Latitudine.ToString());
            xml += Utils.CreateXmlTag("EntUsc", Utils.ConvertiBoolToIntString(EntUsc));
            xml += Utils.CreateXmlTag("DataOra", Utils.ConvertDateTime(DateTime.Now));
            xml += Utils.CreateXmlTag("DaData", Utils.ConvertDateTime(new DateTime(DaData.Year, DaData.Month, DaData.Day, 00, 00, 00)));
            xml += Utils.CreateXmlTag("AData", Utils.ConvertDateTime(new DateTime(AData.Year, AData.Month, AData.Day, 23, 59, 59))); 
            xml += Utils.CreateXmlTag("Modalita", Modalita);
            

            xml += "</params>";

            return xml;
        }
    }
}
