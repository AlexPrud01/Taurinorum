using System;
using System.Collections.Generic;

namespace Taurinorum.Object.FilterModel.EmissioneFoglio
{
    public class ClienteFM : BaseFM
    {
        public int IDIndirizzo { get; set; }
        public string StrIDIndirizzo
        {
            get
            {
                return IDIndirizzo.ToString();
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    IDIndirizzo = int.Parse(value);
            }
        }
        public string Nome { get; set; } = "";
        public string Cognome { get; set; } = "";
        public string Denominazione { get; set; } = "";
    }
}
