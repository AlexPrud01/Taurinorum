//using Microsoft.AspNetCore.Components;
//using Microsoft.JSInterop;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Taurinorum.Object.DataTrasportObject;

//namespace Taurinorum.Center.Pages
//{
//    public partial class Dipendenti
//    {
//        [Inject]
//        IJSRuntime JsRuntime { get; set; }
//        private List<UtenteDTO> listUtente = new List<UtenteDTO>();
//        private RestService restService = new RestService();
//        private UtenteDTO utente = new UtenteDTO();
//        private int NumeroDipendenti = 0;

//        protected override async Task OnInitializedAsync()
//        {
//            listUtente = await restService.Utente_GetListAsync(null);
//            NumeroDipendenti = listUtente.Count();
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

//        private void DettaglioDipendente(int ID)
//        {
//            PassaggioID(ID);
//            NavManager.NavigateTo("/DettaglioDipendente");
//        }

//        private async Task PassaggioID(int ID)
//        {
//            await ProtectedSessionStore.SetAsync("IdDipendente", ID);
//        }
//    }
//}
