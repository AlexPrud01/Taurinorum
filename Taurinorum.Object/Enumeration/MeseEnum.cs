using System;
using System.ComponentModel.DataAnnotations;

namespace Taurinorum.Object.FilterModel
{
    public enum MeseEnum
    {
        [Display(Name = "Gennaio")]
        Gennaio = 1,

        [Display(Name = "Febbraio")]
        Febbraio = 2,

        [Display(Name = "Marzo")]
        Marzo = 3,

        [Display(Name = "Aprile")]
        Aprile = 4,

        [Display(Name = "Maggio")]
        Maggio = 5,

        [Display(Name = "Giugno")]
        Giugno = 6,

        [Display(Name = "Luglio")]
        Luglio = 7,

        [Display(Name = "Agosto")]
        Agosto = 8,

        [Display(Name = "Settembre")]
        Settembre = 9,

        [Display(Name = "Ottobre")]
        Ottobre = 10,

        [Display(Name = "Novembre")]
        Novembre = 11,

        [Display(Name = "Dicembre")]
        Dicembre = 12
    }
}
