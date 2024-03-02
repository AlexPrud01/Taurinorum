using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Threading.Tasks;
using Taurinorum.Object.DataTrasportObject;
using Taurinorum.Object.FilterModel;

namespace Taurinorum.Center.Pages
{
    public partial class Account
    {
        private UtenteDTO utente = new UtenteDTO();
        private List<CompilazioneOreDTO> listCompilaOre = new List<CompilazioneOreDTO>();
        private List<UtenteGiornoAttivitàDTO> utenteAttività = new List<UtenteGiornoAttivitàDTO>();
        private RestService restService = new RestService();
        private bool visualizzaSettimana = true;
        private bool visualizzaOreLavorate = false;
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            try
            {
                if (utente.ID == 0)
                {
                    var Result = await ProtectedSessionStore.GetAsync<int>("IdUtente");
                    try
                    {
                        utente = await restService.Utente_GetByIdAsync(Result.Value);
                        utenteAttività = await restService.UtenteGiornoAttività_List_GetByIDUtente(utente.ID);
                        StateHasChanged();
                    }
                    catch
                    {
                        NavManager.NavigateTo("");
                    }
                }
            }
            catch (Exception Ex)
            {
            }
        }


        private async void VisualizzaSettimana()
        {
            visualizzaSettimana = true;
            visualizzaOreLavorate = false;
            utenteAttività = await restService.UtenteGiornoAttività_List_GetByIDUtente(utente.ID);
            StateHasChanged();
        }

        private async void VisualizzaOreEffettuateMese()
        {
            visualizzaOreLavorate = true;
            //visualizzaSettimana = false;
            //listCompilaOre = await restService.CompilazioneOre_ListGetByIDUtente(utente.ID);
            StateHasChanged();
        }

        private async Task OperazioneRiuscita()
        {
            bool confirmed = await JsRuntime.InvokeAsync<bool>("confirm", "Operazione riuscita con successo");
        }
    }
}
