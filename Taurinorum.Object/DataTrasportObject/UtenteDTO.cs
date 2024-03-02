using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Taurinorum.Object.DataTrasportObject
{
    public class UtenteDTO
    {
        public int ID { get; set; }
        public string Nome { get; set; } = "";
        public string Cognome { get; set; } = "";
        public decimal PagaOraria { get; set; } = 0;
        public string TipoContratto { get; set; } = "";
        public int NumeroOreLavorativeSettimanali { get; set; } = 0;
        public int NumeroOreLavorativeExtra { get; set; } = 0;
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public bool Admin { get; set; } = false;

    }
}
