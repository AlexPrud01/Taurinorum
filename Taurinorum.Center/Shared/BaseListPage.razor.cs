using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using System.ComponentModel.DataAnnotations;
using Taurinorum.Object;
using Taurinorum.Object.DataTrasportObject;
using Taurinorum.Object.Enumeration;

namespace Taurinorum.Center.Shared
{
    public abstract partial class BaseListPage
    {
        [Inject]
        IJSRuntime JsRuntime { get; set; }
        [Parameter]
        public EventCallback<string> OnClose { get; set; }
        [Parameter]
        public bool SelezionaDett { get; set; } = false;
        public int IDSelezionato { get; set; } = 0;
        public Allert Allert { get; set; }
        public bool Messaggio { get; set; }
        protected virtual RenderFragment? Childcontent => (builder) => this.BuildRenderTree(builder); 
        protected abstract RenderFragment BaseContent { get; }
        public EPagina Pagina = new EPagina();

        public string ciao;
        
        private string lunghezzaModale;
        public string LunghezzaModale
        {
            get => lunghezzaModale;
            set
            {
                // Se il valore è diverso, lo aggiorna e avvia il metodo
                if (lunghezzaModale != value)
                {
                    lunghezzaModale = value;
                    StateHasChanged(); // Avvia il metodo quando il valore cambia
                }
            }
        }
        public BaseListPage()
        {
            _renderFragment = builder =>
            {
                _hasPendingQueuedRender = false;
                _hasNeverRendered = false;
                builder.AddContent(0, BaseContent);
            };
        }
        protected abstract Task Initialize();
        protected abstract Task CaricaLista();
        public async Task Cerca()
        {
            await CaricaLista();
        }
        public async Task Nuovo()
        {
            Modale_IsOpen = true;
        }

        public async Task Seleziona(int? ID)
        {
            OnClose.InvokeAsync(ID.ToString());
        }
        protected async Task ApriModale()
        {
            Modale_IsOpen = true;
        }
        protected async override Task OnInitializedAsync()
        {
            await Initialize();
        }

        public void VisualizzaMessaggio(Allert allert)
        {
            Messaggio = true;
            Allert = allert;
        }
        protected override async Task ChiudiModale(string ID)
        {
            Modale_IsOpen = false;
            if(ID != "0")
                await Seleziona(Convert.ToInt32(ID));
        }
    }
}
