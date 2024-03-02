using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Taurinorum.Object.FilterModel.EmissioneFoglio
{
    public class EmissioneFoglioDiLavoroFM : BaseFM
    {
        public string Nome { get; set; } = "";

        public override string ToXml()
        {
            string xml = "<params>";

            xml += Utils.CreateXmlTag("ID", ID);
            xml += Utils.CreateXmlTag("Codice", Codice);
            xml += Utils.CreateXmlTag("Nome", Nome);
            xml += Utils.CreateXmlTag("Modalita", Modalita);

            xml += "</params>";

            return xml;
        }
    }
}
