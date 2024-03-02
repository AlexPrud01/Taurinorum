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
    public partial class IvaList
    {
        protected override RenderFragment BaseContent => (builder) => base.BuildRenderTree(builder);
        List<IvaDTO> ListIvaDTO = new List<IvaDTO>();
        IvaFM Filtro = new IvaFM();
        

        protected override async Task Initialize()
        {
            Pagina = EPagina.Iva;
            PaginaDaAprire = EPagina.IvaDett;
            await CaricaLista();
        }

        protected override async Task CaricaLista()
        {
            ListIvaDTO = await restService.Iva_GetListAsync(Filtro);
            StateHasChanged();
        }

        public async Task Modifica(int ID)
        {
            IDSelezionato = ID;
            Modale_IsOpen = true;
        }

        public async Task Elimina(int ID)
        {
            var allert = await restService.Iva_DeleteAsync(ID);
            VisualizzaMessaggio(allert);
            CaricaLista();
        }

    }
}
