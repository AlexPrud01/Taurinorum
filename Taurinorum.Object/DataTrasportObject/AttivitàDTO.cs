using System;
using System.Collections.Generic;

namespace Taurinorum.Object.DataTrasportObject
{
    public class AttivitàDTO : BaseDTO
    {
        public int IdAzienda { get; set; }
        public string Nome { get; set; } = "";
        public DateTime TempoDaImpegare { get; set; } = new DateTime();
        public DateTime DataScadenza { get; set; } = DateTime.Now;
        public int nPersone { get; set; } = 1;
        public string Descrizione { get; set; } = "";
        public string Via { get; set; } = "";
        public string Città { get; set; } = "";
        public bool Lunedi { get; set; } = false;
        public bool Martedi { get; set; } = false;
        public bool Mercoledi { get; set; } = false;
        public bool Giovedi { get; set; } = false;
        public bool Venerdi { get; set; } = false;
        public bool Sabato { get; set; } = false;
        public bool Domenica { get; set; } = false;
        public string Ora { get; set; } = "";
        public decimal Prezzo { get; set; } = 0;


    }
}
