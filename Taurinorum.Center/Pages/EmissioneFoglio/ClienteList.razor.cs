using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Taurinorum.Object;
using Taurinorum.Object.DataTrasportObject.EmissioneFoglio;
using Taurinorum.Object.Enumeration;
using Taurinorum.Object.FilterModel;
using Taurinorum.Object.FilterModel.EmissioneFoglio;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Taurinorum.Center.Pages.EmissioneFoglio
{
    public partial class ClienteList
    {
        protected override RenderFragment BaseContent => (builder) => base.BuildRenderTree(builder);
        List<ClienteDTO> ListClienteDTO = new List<ClienteDTO>();
        ClienteFM Filtro = new ClienteFM();
        

        protected override async Task Initialize()
        {
            Pagina = EPagina.Cliente;
            PaginaDaAprire = EPagina.ClienteDett;
            await CaricaLista();
        }

        protected override async Task CaricaLista()
        {
            ListClienteDTO = await restService.Cliente_GetListAsync(Filtro);
            StateHasChanged();
        }

        public async Task Modifica(int ID)
        {
            IDSelezionato = ID;
            Modale_IsOpen = true;
        }

        public async Task Elimina(int ID)
        {
            var allert = await restService.Cliente_DeleteAsync(ID);
            VisualizzaMessaggio(allert);
            CaricaLista();
        }

    }
}
