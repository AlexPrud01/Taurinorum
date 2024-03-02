using Microsoft.JSInterop;
using Taurinorum.Object.DataTrasportObject;
using Taurinorum.Object.FilterModel;

namespace Taurinorum.Center.Pages
{
    public partial class CompilaOre
    {
        List<CompilazioneOreDTO> listCompilaOre = new List<CompilazioneOreDTO>();
        UtenteDTO utente = new UtenteDTO();
        CompilazioneOreFM compilazioneOreFM = new CompilazioneOreFM();
        RestService restService = new RestService();
        MeseEnum meseSelezionato;
        bool visualizzaMese = true;

        protected override async Task OnInitializedAsync()
        {
            meseSelezionato = (MeseEnum)(DateTime.Now.Month);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            try
            {
                int result = 0;
                if (utente.ID == 0)
                {
                    var obj = await ProtectedSessionStore.GetAsync<int>("IdUtente");
                    result = obj.Value;
                }

                if (result != 0)
                {
                    utente = await restService.Utente_GetByIdAsync(result);
                    if (utente != null && utente.ID != 0)
                        compilazioneOreFM.IDUtente = utente.ID;

                    await aggiornaTabella();
                    StateHasChanged();
                }
            }
            catch
            {
                NavManager.NavigateTo("");
            }
        }

        private async Task aggiornaTabella()
        {
            if (visualizzaMese)
                SelezionaMese();

            listCompilaOre = await restService.CompilazioneOre_GetList(compilazioneOreFM);

            calcolaOre(0);
            StateHasChanged();
        }

        private decimal calcolaOre(decimal Tempo)
        {
            decimal tempoImpiegato = Tempo;
            if (Tempo == 0)
                foreach (var oreCompilate in listCompilaOre)
                {
                    tempoImpiegato += oreCompilate.TotaleMinuti;
                }

            return decimal.Round(tempoImpiegato/60, 2);

        }

        private async void Salva()
        {
            try
            {
                compilazioneOreFM.IDUtente = utente.ID;
                await restService.CompilazioneOre_SetAsync(compilazioneOreFM);
                messaggioAttivitaSalvata();
                aggiornaTabella();
                compilazioneOreFM = new CompilazioneOreFM();
            }
            catch
            {
                messaggioAttivitaNonSalvata();
            }

        }

        private async void Elimina(int ID)
        {
            try
            {
                CompilazioneOreDTO compilazioneDaEliminare = await restService.CompilazioneOre_GetByIdAsync(ID);
                await restService.CompilazioneOre_DeleteAsync(compilazioneDaEliminare.ID);
                messaggioAttivitaEliminata();
                aggiornaTabella();
            }
            catch
            {
                messaggioAttivitaNonEliminata();
            }

        }
        private void SelezionaMese()
        {
            compilazioneOreFM.DaData = new DateTime(DateTime.Now.Year, (int)meseSelezionato, 01, 00, 00, 00, 00);
            compilazioneOreFM.AData = new DateTime(DateTime.Now.Year, (int)meseSelezionato, 01, 00, 00, 00, 00).AddMonths(1);
        }

        private async Task messaggioAttivitaEliminata()
        {
            bool confirmed = await JsRuntime.InvokeAsync<bool>("Conferma", "Attività eliminata correttamente, OK");
        }

        private async Task messaggioAttivitaNonEliminata()
        {
            bool confirmed = await JsRuntime.InvokeAsync<bool>("Errore", "Attività eliminata non correttamente, OK");
        }

        private async Task messaggioAttivitaSalvata()
        {
            bool confirmed = await JsRuntime.InvokeAsync<bool>("Conferma", "Attività salvata correttamente, OK");
        }

        private async Task messaggioAttivitaNonSalvata()
        {
            bool confirmed = await JsRuntime.InvokeAsync<bool>("Errore", "Attività salvata non correttamente, OK");
        }
    }
}
