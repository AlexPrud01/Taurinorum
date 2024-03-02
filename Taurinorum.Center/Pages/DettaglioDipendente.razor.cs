//using Microsoft.AspNetCore.Components;
//using Microsoft.JSInterop;
//using System.Threading.Tasks;
//using Taurinorum.Object.DataTrasportObject;
//using Taurinorum.Object.FilterModel;

//namespace Taurinorum.Center.Pages
//{
//    public partial class DettaglioDipendente
//    {
//        [Inject]
//        IJSRuntime JsRuntime { get; set; }
//        private UtenteDTO utenteDTO = new UtenteDTO();
//        private UtenteFM utente = new UtenteFM();
//        private RestService restService = new RestService();

//        protected override async Task OnInitializedAsync()
//        {
//            var Result = await ProtectedSessionStore.GetAsync<int>("IdDipendente");
//            if (Result.Value != 0)
//            {
//                utenteDTO = await restService.Utente_GetByIdAsync(Result.Value);
//            }
//        }

//        protected override async Task OnAfterRenderAsync(bool firstRender)
//        {
//            try
//            {
//                var Result = await ProtectedSessionStore.GetAsync<int>("IdUtente");
//            }
//            catch
//            {
//                NavManager.NavigateTo("");
//            }
//        }

//        public async void Salva()
//        {
//            var obj = await restService.Utente_SetAsync(utente);
//            if (obj.Valore == 0)
//                await JsRuntime.InvokeVoidAsync("Errore", "Salvataggio fallito");

//            NavManager.NavigateTo("/Dipendenti");
//        }
//    }
//}
