using System;

namespace Taurinorum.Object.FilterModel
{
    public class CompilazioneOreFM : BaseFM
    {
        public int ID { get; set; }
        public int IDUtente { get; set; }
        public int IDAzienda { get; set; } = 0;
        public int IDAttività { get; set; } = 0;
        public string Azienda { get; set; } = "";
        public string Attivita { get; set; } = "";
        public DateTime DataOraInizio { get; set; } = DateTime.Now;
        public DateTime DataOraFine { get; set; } = DateTime.Now.AddHours(1);
        public DateTime DaData { get; set; } = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 01, 00, 00, 00, 00);
        public DateTime AData { get; set; } = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 01, 00, 00, 00, 00).AddDays(1);

        public override string ToXml()
        {
            string xml = "<params>";

            xml += Utils.CreateXmlTag("ID", ID);
            xml += Utils.CreateXmlTag("IDUtente", IDUtente);
            xml += Utils.CreateXmlTag("IDAzienda", IDAzienda);
            xml += Utils.CreateXmlTag("IDAttivita", IDAttività);
            xml += Utils.CreateXmlTag("Azienda", Azienda);
            xml += Utils.CreateXmlTag("Attivita", Attivita);
            xml += Utils.CreateXmlTag("DataOraInizio", Utils.ConvertDateTime(DataOraInizio));
            xml += Utils.CreateXmlTag("DataOraFine", Utils.ConvertDateTime(DataOraFine));
            xml += Utils.CreateXmlTag("DaData", Utils.ConvertDateTime(DaData));
            xml += Utils.CreateXmlTag("AData", Utils.ConvertDateTime(AData));

            xml += "</params>";

            return xml;
        }
    }
}
