using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Taurinorum.Object.DataTrasportObject
{
    public class TimbraturaDTO
    {
        public int ID { get; set; }
        public int IDUtente { get; set; }
        public decimal LongitudineEntrata { get; set; }
        public decimal LatitudineEntrata { get; set; }
        public decimal LongitudineUscita { get; set; }
        public decimal LatitudineUscita { get; set; }
        public DateTime DataOraEntrata { get; set; }
        public DateTime DataOraUscita { get; set; }
        public int TotaleOre { get; set; }
    }
}
