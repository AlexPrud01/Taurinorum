using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Taurinorum.Object;
using Taurinorum.Object.DataTrasportObject;
using Taurinorum.Object.FilterModel;
using Taurinorum.Object.FilterModel.EmissioneFoglio;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Taurinorum.Center.Pages
{
    public partial class Timbrature
    {
        [Inject]
        IJSRuntime JsRuntime { get; set; }
        TimbraFM timbraFM = new TimbraFM();
        UtenteDTO utente = new UtenteDTO();
        private GestoreExcel GestoreExcel = new GestoreExcel();
        UtenteDTO utenteRicercaDTO = new UtenteDTO();
        RestService restService = new RestService();
        List<UtenteDTO> ListUtenteDTO = new List<UtenteDTO>();
        List<TimbraturaDTO> ListUtenteTimbratureDTO = new List<TimbraturaDTO>();
        bool visualizzaMese = true;
        MeseEnum meseSelezionato;
        int annoSelezionato;
        int TotaleOre;
        int TotaleMinuti;

        protected override async Task OnInitializedAsync()
        {
            if(DateTime.Now.Month == 1)
            {
                meseSelezionato = (MeseEnum)(12);
                annoSelezionato = DateTime.Now.Year - 1;
            }
            else
            {
                meseSelezionato = (MeseEnum)(DateTime.Now.Month - 1);
                annoSelezionato = DateTime.Now.Year;
            }
            ListUtenteDTO = await restService.Utente_GetListAsync(new UtenteFM()); 
        }


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            try
            {
                if(utente.ID == 0)
                {
                    var Result = await ProtectedSessionStore.GetAsync<int>("IdUtente");
                    try
                    {
                        utente = await restService.Utente_GetByIdAsync(Result.Value);
                    }
                    catch
                    {
                        utente = new UtenteDTO();
                    }
                    await Cerca();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void StampaExcel()
        {
            //var datiExcel = GestoreExcel.CreaFileExcel();

            // Scarica il file Excel nel browser
            //var nomeFile = "esempio.xlsx";
            //var tipoMime = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //var base64Data = Convert.ToBase64String(datiExcel);

            //NavManager.NavigateTo($"data:{tipoMime};base64,{base64Data}", true);
        }

        protected async Task<IEnumerable<UtenteDTO>> CercaUtente(string searchText)
        {
            if (searchText == "#")
                return ListUtenteDTO;

            if (searchText.Length > 2)
            {
                return ListUtenteDTO.Where(x => x.Nome.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0 || 
                                                x.Cognome.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            }
            return new List<UtenteDTO>();

        }

        private string InOut(bool EntrataUscita)
        {
            if (EntrataUscita)
                return "Entrata";
            else
                return "Uscita";
        }

        private async void Posizione(decimal Longitudine, decimal Latitudine)
        {
            await JsRuntime.InvokeVoidAsync("open", "https://www.google.it/maps/@" + Latitudine + "," + Longitudine + ",16z?entry=ttu", "_blank");

        }

        public async Task Cerca()
        {
            try
            {
                if (visualizzaMese)
                    SelezionaMese();

                if(!utente.Admin)
                    timbraFM.IDUtente = utente.ID;
                else
                    if (utenteRicercaDTO.ID != 0)
                        timbraFM.IDUtente = utenteRicercaDTO.ID;

                ListUtenteTimbratureDTO = await restService.Timbratura_GetListAsync(timbraFM);

                CalcolaOre();

                if (ListUtenteTimbratureDTO == null)
                    await JsRuntime.InvokeVoidAsync("Errore", "Ricerca fallita");
            }
            catch
            {
                await JsRuntime.InvokeVoidAsync("Errore", "Ricerca fallita");
            }

            StateHasChanged();
        }

        private void SelezionaMese()
        {
            if ((int)meseSelezionato == 12)
            {
                timbraFM.AData = new DateTime(annoSelezionato + 1, 01, 01, 00, 00, 00);
                timbraFM.DaData = (new DateTime(annoSelezionato, (int)meseSelezionato, 01, 00, 00, 00));
            }
            else
            {
                timbraFM.AData = (new DateTime(annoSelezionato, (int)meseSelezionato + 1, 01, 00, 00, 00));
                timbraFM.DaData = (new DateTime(annoSelezionato, (int)meseSelezionato, 01, 00, 00, 00));
            }
        }

        private void CalcolaOre()
        {
            try
            {
                TotaleMinuti = 0; 
                TotaleOre = 0;
                foreach (TimbraturaDTO CalcoloMinuti in ListUtenteTimbratureDTO)
                {
                    TotaleMinuti += CalcoloMinuti.TotaleOre;
                    while (TotaleMinuti > 59)
                    {
                        TotaleOre++;
                        TotaleMinuti = TotaleMinuti - 60;
                    }
                }
            }
            catch (Exception) 
            {

            }
        }

        public string tabOre(int tempoMin)
        {
            double risultato = (double)tempoMin / 60;
            return risultato.ToString("F2");
        }

    }
}
