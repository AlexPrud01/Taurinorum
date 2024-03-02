using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Taurinorum.Object.DataTrasportObject
{
    public class OrdineDTO : BaseDTO
    {
        public int Quantita { get; set; } = 0;
        public decimal ImportoTot { get; set; } = 0;
        public int IDIva { get; set; } = 0;
    }
}
