using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Taurinorum.Object.DataTrasportObject.EmissioneFoglio
{
    public class AttivitaCedutaDTO : AttivitàDTO
    {
        public override string ToXml()
        {
            string xml = "<params>";

            xml += Utils.CreateXmlTag("ID", ID);
            xml += Utils.CreateXmlTag("Codice", Codice);
            xml += Utils.CreateXmlTag("Descrizione", Descrizione);
            xml += Utils.CreateXmlTag("Prezzo", Utils.ConvertiDecimal(Prezzo));

            xml += "</params>";

            return xml;
        }
    }
}
