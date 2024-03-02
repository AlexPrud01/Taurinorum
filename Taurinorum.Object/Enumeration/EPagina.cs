using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Taurinorum.Object.Enumeration
{
    public enum EPagina
    {
        [Display(Name = "Pagina Generica")]
        Generica,
        [Display(Name = "Attività Ceduta")]
        AttivitaCeduta,
        [Display(Name = "Dettaglio Attività Ceduta")]
        AttivitaCedutaDett,
        [Display(Name = "Fornitore")]
        Fornitore,
        [Display(Name = "Dettaglio Fornitore")]
        FornitoreDett,
        [Display(Name = "Cliente")]
        Cliente,
        [Display(Name = "Dettaglio Cliente")]
        ClienteDett,
        [Display(Name = "Attività")]
        Attivita,
        [Display(Name = "Dettaglio Attività")]
        AttivitaDett,
        [Display(Name = "IVA")]
        Iva,
        [Display(Name = "Dettaglio IVA")]
        IvaDett,
        [Display(Name = "Modalità di Pagamento")]
        ModalitaPagamento,
        [Display(Name = "Dettaglio Modalità di Pagamento")]
        ModalitaPagamentoDett
    }
}
