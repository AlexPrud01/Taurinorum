using System;
using System.Collections.Generic;

namespace Taurinorum.Object.FilterModel.EmissioneFoglio
{
    public class FornitoreFM : BaseFM
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
        public string PartitaIva { get; set; } = "";
        public string CodiceFiscale { get; set; } = "";
        public string CodiceSDI { get; set; } = "";
    }
}
