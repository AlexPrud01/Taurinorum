namespace Taurinorum.Object.FilterModel
{
    public class UtenteFM:BaseFM
    {
        public string Nome { get; set; } = "";
        public string Cognome { get; set; } = "";
        public double PagaOraria { get; set; } = 0;
        public string TipoContratto { get; set; } = "";
        public int NumeroOreLavorativeSettimanali { get; set; } = 0;
        public int NumeroOreLavorativeExtra { get; set; } = 0;
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public bool Admin { get; set; } = false;

        public override string ToXml()
        {
            string xml = "<params>";

            xml += Utils.CreateXmlTag("ID", ID.ToString());
            xml += Utils.CreateXmlTag("Nome", Nome);
            xml += Utils.CreateXmlTag("Cognome", Cognome);
            xml += Utils.CreateXmlTag("PagaOraria", PagaOraria.ToString());
            xml += Utils.CreateXmlTag("TipoContratto", TipoContratto);
            xml += Utils.CreateXmlTag("NumeroOreLavorativeSettimanali", NumeroOreLavorativeSettimanali.ToString());
            xml += Utils.CreateXmlTag("NumeroOreLavorativeExtra", NumeroOreLavorativeExtra.ToString());
            xml += Utils.CreateXmlTag("Username", Username);
            xml += Utils.CreateXmlTag("Password", Password);
            xml += Utils.CreateXmlTag("Admin", Utils.ConvertiBoolToIntString(Admin));


            xml += "</params>";

            return xml;
        }

    }
}
