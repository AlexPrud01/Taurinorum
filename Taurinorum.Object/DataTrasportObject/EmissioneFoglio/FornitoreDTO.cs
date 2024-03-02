using System;
using System.Collections.Generic;

namespace Taurinorum.Object.DataTrasportObject.EmissioneFoglio
{
    public class FornitoreDTO : BaseDTO
    {
        public int IDIndirizzo { get; set; } = 0;
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
        public string PartitaIva { get; set; } = "";
        public string CodiceFiscale { get; set; } = "";
        public string CodiceSDI { get; set; } = "";
        public string Indirizzo_Codice { get; set; } = "";
        public string Indirizzo_Descrizione { get; set; } = "";
        public string Indirizzo_Via { get; set; } = "";
        public string Indirizzo_Citta { get; set; } = "";
        public string Indirizzo_Cap { get; set; } = "";
        public string Indirizzo_Provincia { get; set; } = "";
        public string Indirizzo_Nazione { get; set; } = "";
        public string Indirizzo_Regione { get; set; } = "";
    }
}
