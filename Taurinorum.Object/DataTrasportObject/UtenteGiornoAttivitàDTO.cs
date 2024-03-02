using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taurinorum.Object.DataTrasportObject
{
    public class UtenteGiornoAttivitàDTO
    {
        public int ID { get; set; }
        public int IdAttività { get; set; }
        public int IdUtente { get; set; } = 0;
        public bool Lunedi { get; set; } = false;
        public bool Martedi { get; set; } = false;
        public bool Mercoledi { get; set; } = false;
        public bool Giovedi { get; set; } = false;
        public bool Venerdi { get; set; } = false;
        public bool Sabato { get; set; } = false;
        public bool Domenica { get; set; } = false;
        public decimal Paga { get; set; } = 0;

        public UtenteDTO utente { get; set; }
        public AttivitàDTO attività { get; set; }
    }
}
