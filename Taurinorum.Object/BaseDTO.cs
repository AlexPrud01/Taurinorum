using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security;

namespace Taurinorum.Object
{
    public abstract class BaseDTO
    {
        string xmlParams = String.Empty;
        public int? MaxRecord { get; set; }
        /// <summary>
        /// 0 = recordset lista, 1 = recordset dettaglio
        /// </summary>
        public int Modalita { get; set; } = 0;
        public int ID { get; set; } = 0;
        public string StrID
        {
            get
            {
                if (ID!=null)
                    return ID.ToString();
                return string.Empty;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    ID = int.Parse(value);
            }
        }
        public string Codice { get; set; }
        public string Descrizione { get; set; }

        /// <summary>
        /// Il metodo converte i filtri in nodi XML 
        /// </summary>
        public virtual string ToXml()
        {
            xmlParams = "<params>";
            List<PropertyInfo> properties = GetType().GetProperties().ToList();

            foreach (PropertyInfo property in properties)
            {
                string propertyName = property.Name;
                var propertyValue = property.GetValue(this);
                if (propertyValue != null && !string.IsNullOrEmpty(propertyValue.ToString()))
                {
                    string campoNormalizzato = SecurityElement.Escape(propertyValue.ToString().Trim());  //propertyValue.ToString().Replace("<", "").Replace(">", "").Replace("&", "");

                    xmlParams += string.Format("<{0}>{1}</{0}>", propertyName, campoNormalizzato);

                }
            }
            xmlParams += "</params>";

            return xmlParams;
        }
    }
}
