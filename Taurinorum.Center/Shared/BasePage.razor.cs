using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using System.ComponentModel.DataAnnotations;
using Taurinorum.Object.DataTrasportObject;
using Taurinorum.Object.Enumeration;

namespace Taurinorum.Center.Shared
{
    public abstract partial class BasePage
    {
        public UtenteDTO utente = new UtenteDTO();
        public RestService restService = new RestService();
        public EPagina PaginaDaAprire = new EPagina();
        public bool Modale_IsOpen = false;

        protected virtual async Task ChiudiModale(string ID)
        {
            Modale_IsOpen = false;
        }
    }
}
