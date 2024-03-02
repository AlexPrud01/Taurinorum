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
    public partial class FornitoreList
    {
        protected override RenderFragment BaseContent => (builder) => base.BuildRenderTree(builder);
        List<FornitoreDTO> ListFornitoreDTO = new List<FornitoreDTO>();
        FornitoreFM Filtro = new FornitoreFM();
        

        protected override async Task Initialize()
        {
            Pagina = EPagina.Fornitore;
            PaginaDaAprire = EPagina.FornitoreDett;
            await CaricaLista();
        }

        protected override async Task CaricaLista()
        {
            ListFornitoreDTO = await restService.Fornitore_GetListAsync(Filtro);
            StateHasChanged();
        }

        public async Task Modifica(int ID)
        {
            IDSelezionato = ID;
            Modale_IsOpen = true;
        }

        public async Task Elimina(int ID)
        {
            var allert = await restService.Fornitore_DeleteAsync(ID);
            VisualizzaMessaggio(allert);
            CaricaLista();
        }

    }
}
