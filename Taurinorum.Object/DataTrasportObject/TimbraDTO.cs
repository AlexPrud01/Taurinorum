using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Taurinorum.Object.DataTrasportObject
{
    public class TimbraDTO
    {
        public int ID { get; set; }
        public int IDUtente { get; set; }
        public decimal Longitudine { get; set; }
        public decimal Latitudine { get; set; }
        public bool EntUsc { get; set; }
        public DateTime DataOra { get; set; }
    }
}
