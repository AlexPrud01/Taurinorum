using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Taurinorum.Object.DataTrasportObject.EmissioneFoglio
{
    public class EmissioneFoglioDiLavoroDTO : BaseDTO
    {
        public int IDFornitore { get; set; }
        public string StrIDFornitore
        {
            get
            {
                return IDFornitore.ToString();
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    IDFornitore = int.Parse(value);
            }
        }
        public int IDCliente { get; set; }
        public string StrIDCliente
        {
            get
            {
                return IDCliente.ToString();
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    IDCliente = int.Parse(value);
            }
        }
        public int IDAttivita { get; set; }
        public string StrIDAttivita
        {
            get
            {
                return IDAttivita.ToString();
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    IDAttivita = int.Parse(value);
            }
        }
        public int IDIva { get; set; }
        public string StrIDIva
        {
            get
            {
                return IDIva.ToString();
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    IDIva = int.Parse(value);
            }
        }
        public int IDModalitaPagamento { get; set; }
        public string StrIDModalitaPagamento
        {
            get
            {
                return IDModalitaPagamento.ToString();
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    IDModalitaPagamento = int.Parse(value);
            }
        }

        public decimal Totale { get; set; }
        public FornitoreDTO Fornitore { get; set; }
        public ClienteDTO Cliente { get; set; }
        public AttivitàDTO Attivita { get; set; }
        public IvaDTO Iva { get; set; }
        public ModalitaPagamentoDTO ModalitaPagamento { get; set; }


        public override string ToXml()
        {
            string xml = "<params>";

            xml += Utils.CreateXmlTag("ID", ID);
            xml += Utils.CreateXmlTag("Codice", Codice);
            xml += Utils.CreateXmlTag("Modalita", Modalita);
            xml += Utils.CreateXmlTag("IDFornitore", IDFornitore);
            xml += Utils.CreateXmlTag("IDCliente", IDCliente);
            xml += Utils.CreateXmlTag("IDAttivita", IDAttivita);
            xml += Utils.CreateXmlTag("IDIva", IDIva);
            xml += Utils.CreateXmlTag("IDModalitaPagamento", IDModalitaPagamento);
            xml += Utils.CreateXmlTag("Totale", Utils.ConvertiDecimal(Totale));

            xml += "</params>";

            return xml;
        }
    }
}
