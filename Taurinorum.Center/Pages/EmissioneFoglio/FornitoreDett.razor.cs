using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
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
    public partial class FornitoreDett
    {
        protected override RenderFragment BaseContent => (builder) => base.BuildRenderTree(builder);
        FornitoreDTO Dettaglio = new FornitoreDTO();

        #region Metodi 
        protected override async void Salva()
        {
            try
            {
                //AlertService.Clear();
                //IsBusy = true;

                if (string.IsNullOrEmpty(Dettaglio.Codice))
                {
                    VisualizzaMessaggio(new Allert() { Valore = -1, Messaggio = "Il campo \"Codice\" è obbligatorio" });
                    return;
                }
                if (string.IsNullOrEmpty(Dettaglio.Descrizione))
                {
                    VisualizzaMessaggio(new Allert() { Valore = -1, Messaggio = "Il campo \"Descrizione\" è obbligatorio" });
                    return;
                }
                var ret = await restService.Fornitore_SetAsync(Dettaglio);

                if (ret.Valore != null && ret.Valore < 0)
                {
                    VisualizzaMessaggio(new Allert() { Valore = 0, Messaggio = ret.Messaggio });
                }
                else
                {
                    VisualizzaMessaggio(new Allert() { Valore = 1, Messaggio = ret.Messaggio });
                    ID = ret.Valore;
                    Seleziona();
                }
            }
            catch (Exception ex)
            {
                VisualizzaMessaggio(new Allert() { Valore = 0, Messaggio = "Operazione fallita : " + ex.Message});
            }
            finally
            {
                StateHasChanged();
            }
        }
        protected override async Task Initialize()
        {
            Pagina = EPagina.FornitoreDett;
            try
            {
                if (ID != null && ID != 0)
                {
                    var result = await restService.Fornitore_GetByIdAsync(ID);
                    if (result != null)
                        Dettaglio = (FornitoreDTO)result;
                    else
                        VisualizzaMessaggio(new Allert() { Valore = 0, Messaggio = "Fornitore non trovata" });
                    await JsRuntime.InvokeVoidAsync("Errore", "Fornitore non trovata");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public async Task Seleziona()
        {
            OnClose.InvokeAsync(ID.ToString());
        }

        #endregion

    }
}
