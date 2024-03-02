using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Taurinorum.Object.DataTrasportObject
{
    public class CompilazioneOreDTO : BaseDTO
    {
        public int IdUtente { get; set; }
        public string Azienda { get; set; } = "";
        public string Attività { get; set; } = "";
        public DateTime DataOraInizio { get; set; } = DateTime.Now;
        public DateTime DataOraFine { get; set; } = DateTime.Now;
        public int TotaleMinuti { get; set; }
    }
}
