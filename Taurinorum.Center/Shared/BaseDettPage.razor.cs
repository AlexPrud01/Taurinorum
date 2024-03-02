using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using System.ComponentModel.DataAnnotations;
using Taurinorum.Object;
using Taurinorum.Object.DataTrasportObject;
using Taurinorum.Object.Enumeration;

namespace Taurinorum.Center.Shared
{
    public abstract partial class BaseDettPage
    {
        [Inject]
        public IJSRuntime JsRuntime { get; set; }
        [Parameter]
        public EventCallback<string> OnClose { get; set; }
        [Parameter]
        public int ID { get; set; }
        [Parameter]
        public bool SelezionaDett { get; set; } = false;
        public Allert Allert { get; set; }
        public bool Messaggio { get; set; }
        public bool InModifica { get; set; } = false;
        protected virtual RenderFragment? Childcontent => (builder) => this.BuildRenderTree(builder); 
        protected abstract RenderFragment BaseContent { get; }
        public EPagina Pagina = new EPagina();
        public string ciao;

        public BaseDettPage()
        {
            _renderFragment = builder =>
            {
                _hasPendingQueuedRender = false;
                _hasNeverRendered = false;
                builder.AddContent(0, BaseContent);
            };
        }
        protected abstract Task Initialize();
        protected abstract void Salva();
        public void Modifica() { }
        public async Task Cerca()
        {
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
    }
}
