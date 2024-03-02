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
    public partial class ModalitaPagamentoList
    {
        protected override RenderFragment BaseContent => (builder) => base.BuildRenderTree(builder);
        List<ModalitaPagamentoDTO> ListModalitaPagamentoDTO = new List<ModalitaPagamentoDTO>();
        ModalitaPagamentoFM Filtro = new ModalitaPagamentoFM();
        

        protected override async Task Initialize()
        {
            Pagina = EPagina.ModalitaPagamento;
            PaginaDaAprire = EPagina.ModalitaPagamentoDett;
            await CaricaLista();
        }

        protected override async Task CaricaLista()
        {
            ListModalitaPagamentoDTO = await restService.ModalitaPagamento_GetListAsync(Filtro);
            StateHasChanged();
        }

        public async Task Modifica(int ID)
        {
            IDSelezionato = ID;
            Modale_IsOpen = true;
        }

        public async Task Elimina(int ID)
        {
            var allert = await restService.ModalitaPagamento_DeleteAsync(ID);
            VisualizzaMessaggio(allert);
            CaricaLista();
        }

    }
}
