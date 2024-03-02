using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Taurinorum.Object.DataTrasportObject;
using Taurinorum.Object.Enumeration;
using Taurinorum.Object.DataTrasportObject.EmissioneFoglio;
//using PdfSharp.Pdf.AcroForms;
//using PdfSharp.Pdf.IO;
//using PdfSharp.Pdf;
//using PdfSharp.Fonts;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using iTextSharp.text.pdf;

namespace Taurinorum.Center.Pages.EmissioneFoglio
{
    public partial class EmissioneOrdineDiLavoro
    {
        EmissioneFoglioDiLavoroDTO emissioneFoglioDiLavoroDTO = new EmissioneFoglioDiLavoroDTO();
        UtenteDTO utente = new UtenteDTO();
        RestService restService = new RestService();
        GestoreExcel gestoreExcell = new GestoreExcel();

        //public void CompilaPdf()
        //{
        //    try
        //    {
        //        PdfDocument document = PdfReader.Open("wwwroot/NewPortableDocument1.pdf", PdfDocumentOpenMode.Modify);

        //        // Accedi al campo di testo desiderato
        //        PdfTextField textField = document.AcroForm.Fields["Text6"] as PdfTextField;

        //        // Imposta il valore del campo
        //        textField.Value = new PdfString("false");

        //        // Salva il documento
        //        document.Save("wwwroot/nome_del_tuo_file_modificato.pdf");
        //    }
        //    catch(Exception ex)
        //    {

        //    }
        //}

        public static void FillPdf()
        {
            // Caricare il documento PDF
            //using (var reader = new PdfReader("wwwroot/NewPortableDocument1.pdf"))
            //{
            //    // Creare un nuovo documento PDF
            //    using (var ms = new MemoryStream())
            //    {
            //        var stamper = new PdfStamper(reader, ms);

            //        // Ottenere il modulo AcroFields
            //        var acroFields = stamper.AcroFields;


            //        acroFields.SetField("Text6", "Ciao");
            //        // Compilare i campi
            //        //foreach (var field in fieldValues)
            //        //{
            //        //    acroFields.SetField(field.Key, field.Value);
            //        //}

            //        // Stampare il nuovo documento PDF
            //        stamper.Close();

            //        // Salvare il nuovo documento PDF
            //        File.WriteAllBytes("wwwroot/nome_del_tuo_file_modificato.pdf", ms.ToArray());
            //    }
            //}
        }

        protected override async Task OnInitializedAsync()
        {
            FillPdf();
        }

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
                    }
                    catch
                    {
                        utente = new UtenteDTO();
                    }
                    StateHasChanged();
                    
                }
            }
            catch (Exception ex)
            {
            }
        }

        private async void Stampa()
        {
            emissioneFoglioDiLavoroDTO.Cliente = await restService.Cliente_GetByIdAsync(emissioneFoglioDiLavoroDTO.IDCliente);
            emissioneFoglioDiLavoroDTO.Fornitore = await restService.Fornitore_GetByIdAsync(emissioneFoglioDiLavoroDTO.IDFornitore);
            emissioneFoglioDiLavoroDTO.ModalitaPagamento = await restService.ModalitaPagamento_GetByIdAsync(emissioneFoglioDiLavoroDTO.IDModalitaPagamento);
            emissioneFoglioDiLavoroDTO.Iva = await restService.Iva_GetByIdAsync(emissioneFoglioDiLavoroDTO.IDIva);
            var datiExcel = gestoreExcell.CreaFileExcel(emissioneFoglioDiLavoroDTO);

            // Scarica il file Excel nel browser
            var nomeFile = "esempio.xlsx";
            var tipoMime = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            var base64Data = Convert.ToBase64String(datiExcel);

            NavManager.NavigateTo($"data:{tipoMime};base64,{base64Data}", true);
        }
    }
}
