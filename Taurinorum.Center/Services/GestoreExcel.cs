using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Taurinorum.Object.DataTrasportObject.EmissioneFoglio;

namespace Taurinorum.Center
{
    public class GestoreExcel
    {
        private string percorsoFile;

        public GestoreExcel()
        {
            percorsoFile = "ProvaCreazioneOrario.xlsx";
        }

        public byte[] ModificaFileExcel()
        {
            using (var package = new ExcelPackage())
            {
                var foglioLavoro = package.Workbook.Worksheets.Add("Foglio1");

                foglioLavoro.Cells.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                foglioLavoro.Column(1).Width = 33.60;
                foglioLavoro.Row(1).Height = 40.5;
                foglioLavoro.Row(2).Height = 37.5;
                foglioLavoro.Row(3).Height = 34.5;
                foglioLavoro.Cells[1, 1].Style.Font.Size = 26;
                foglioLavoro.Cells[2, 1].Style.Font.Size = 18;
                foglioLavoro.Cells[3, 1].Style.Font.Size = 16;

                // Modifica i dati nel file Excel
                foglioLavoro.Cells[2, 1].Value = "Alessandro Prudente";
                foglioLavoro.Cells[3, 1].Value = "320";

                for (int i = 1; i < 32; i++)
                {
                    foglioLavoro.Column(i + 1).Width = 3.86;
                    foglioLavoro.Cells[2, +(i + 1)].Value = i;
                    foglioLavoro.Cells[2, +(i + 1)].Style.Font.Size = 11;
                    foglioLavoro.Cells[3, i + 1].Style.Font.Size = 11;
                    foglioLavoro.Cells[3, i + 1].Value = 10;
                }

                // Unisci le celle dalla colonna A alla colonna C, per le prime cinque righe
                var rangeDaUnire = foglioLavoro.Cells[1, 1, 1, 32];
                rangeDaUnire.Merge = true;
                rangeDaUnire.Style.Font.Size = 26;
                rangeDaUnire.Value = "Gennaio"; // Puoi impostare il testo che vuoi mostrare nella cella unita

                // Salva il file Excel in uno stream di memoria
                var memoryStream = new MemoryStream();
                package.SaveAs(memoryStream);

                // Restituisci lo stream di memoria come array di byte
                return memoryStream.ToArray();
            }
        }

        public byte[] CreaFileExcel(EmissioneFoglioDiLavoroDTO EmissioneFoglioDiLavoroDTO)
        {
            using (var package = new ExcelPackage(new FileStream("wwwroot/DocumentoBaseTaurinorum.xlsx", FileMode.Open)))
            {
                var foglioLavoro = package.Workbook.Worksheets["Foglio1"];

                foglioLavoro.Cells.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                // Modifica i dati nel file Excel

                //Fornitore
                foglioLavoro.Cells[10, 2].Value = EmissioneFoglioDiLavoroDTO.Fornitore.Nome;
                foglioLavoro.Cells[11, 2].Value = EmissioneFoglioDiLavoroDTO.Fornitore.Indirizzo_Via;
                foglioLavoro.Cells[12, 2].Value = EmissioneFoglioDiLavoroDTO.Fornitore.Indirizzo_Cap + " " + EmissioneFoglioDiLavoroDTO.Cliente.Indirizzo_Citta + " " + EmissioneFoglioDiLavoroDTO.Cliente.Indirizzo_Provincia;

                //Cliente
                foglioLavoro.Cells[10, 8].Value = EmissioneFoglioDiLavoroDTO.Cliente.Nome;
                foglioLavoro.Cells[11, 8].Value = EmissioneFoglioDiLavoroDTO.Cliente.Indirizzo_Via;
                foglioLavoro.Cells[12, 8].Value = EmissioneFoglioDiLavoroDTO.Cliente.Indirizzo_Cap + " " + EmissioneFoglioDiLavoroDTO.Cliente.Indirizzo_Citta + " " + EmissioneFoglioDiLavoroDTO.Cliente.Indirizzo_Provincia;

                foglioLavoro.Cells[14, 4].Value = 123  /*EmissioneFoglioDiLavoroDTO.Codice*/;
                foglioLavoro.Cells[14, 4].Value = DateTime.Now.Year;
                foglioLavoro.Cells[14, 10].Value = DateTime.Now.ToString("dd/MM/yyyy");

                foglioLavoro.Cells[49, 3].Value = EmissioneFoglioDiLavoroDTO.ModalitaPagamento.Descrizione;
                foglioLavoro.Cells[52, 3].Value = EmissioneFoglioDiLavoroDTO.Iva.Descrizione;

                // Salva il file Excel in uno stream di memoria
                var memoryStream = new MemoryStream();
                package.SaveAs(memoryStream);

                // Restituisci lo stream di memoria come array di byte
                return memoryStream.ToArray();
            }
        }
    }
}