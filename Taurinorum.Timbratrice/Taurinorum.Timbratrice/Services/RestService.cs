using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using Taurinorum.Object;
using Taurinorum.Object.DataTrasportObject;
using Taurinorum.Object.FilterModel;
using Taurinorum.Object.FilterModel.EmissioneFoglio;

namespace Taurinorum.Timbratrice.Services
{
    public class RestService
    {
        private HttpClient _client; 
        string BaseAddress = " http://192.168.8.168:3000/";
        public RestService()
        {
            Init();
        }

        private void Init()
        {
            HttpClientHandler clientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
                {
                    return true;
                }
            };

            _client = new HttpClient(clientHandler);
        }

        #region <<UTENTE>>
        public async Task<List<UtenteDTO>> Utente_GetListAsync(UtenteFM? utenteFM)
        {
            string filtri = "";

            if (filtri != null)
            {
                filtri = "/" + utenteFM.Modalita + "/" + utenteFM.ID + "/" + utenteFM.Username + "/" + utenteFM.Password;
            }

            Uri uri = new Uri(BaseAddress + "Service/UtenteGetList" + filtri);

            HttpResponseMessage response;
            try
            {
                response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<UtenteDTO>>(content);
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return null;
            }
            return null;
        }
        public async Task<UtenteDTO?> Utente_GetByIdAsync(int id)
        {
            Uri uri = new Uri(string.Format(BaseAddress + "Service/UtenteGetByID/{0}", id));
            HttpResponseMessage response;
            try
            {
                response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<UtenteDTO> (content);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            return null;
        }
        public async Task<UtenteDTO> Utente_GetByUserAndPasswordAsync(UtenteFM utenteFM)
        {
            string filtri = "";

            if (filtri != null)
            {
                filtri = "/" + utenteFM.Username + "/" + utenteFM.Password;
            }

            Uri uri = new Uri(string.Format(BaseAddress + "Service/UtenteGetUserPassword" + filtri));
            HttpResponseMessage response;
            try
            {
                response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    UtenteDTO utenteDTO = JsonConvert.DeserializeObject<UtenteDTO>(content);
                    return utenteDTO;
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return null;
            }
            return null;
        }
        public async Task<(string returnMessage, int? Result)> Utente_SetAsync(UtenteDTO Item)
        {
            string json = JsonConvert.SerializeObject(Item);
            StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            Uri uri = new Uri(string.Format(BaseAddress + "Service/UtenteSet"));
            HttpResponseMessage response;
            try
            {
                response = await _client.PostAsync(uri, httpContent);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<(string returnMessage, int? Result)>(content);
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return ("errore", 0);
            }
            return ("errore", 0);
        }
        public async Task<string> Utente_DeleteAsync(int IdUtente)
        {
            Uri uri = new Uri(string.Format(BaseAddress + "Service/UtenteDel/{0}", IdUtente));
            HttpResponseMessage response;
            try
            {
                response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<string>(content);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            return null;
        }
        #endregion


        #region <<TIMNRATRICE>>
        public async Task<List<TimbraDTO>> Timbra_GetListAsync(UtenteFM? utenteFM)
        {
            string filtri = "";

            if (filtri != null)
            {
                filtri = "/" + utenteFM.Modalita + "/" + utenteFM.ID + "/" + utenteFM.Username + "/" + utenteFM.Password;
            }

            Uri uri = new Uri(BaseAddress + "Service/TimbraGetList" + filtri);

            HttpResponseMessage response;
            try
            {
                response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<TimbraDTO>>(content);
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return null;
            }
            return null;
        }
        public async Task<TimbraDTO> Timbra_GetByIdUtenteAsync(int id)
        {
            Uri uri = new Uri(string.Format(BaseAddress + "Service/UltimaTimbratura/{0}", id));
            HttpResponseMessage response;
            try
            {
                response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<TimbraDTO>(content);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            return null;
        }
        public async Task<Allert> Timbra_SetAsync(TimbraFM timbraFM)
        {
            string json = JsonConvert.SerializeObject(timbraFM);
            StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            Uri uri = new Uri(string.Format(BaseAddress + "Service/Timbra"));
            HttpResponseMessage response;
            try
            {
                response = await _client.PostAsync(uri, httpContent);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Allert>(content);
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return new Allert() { Messaggio = "errore", Valore = 0 };
            }
            return new Allert() { Messaggio = "errore", Valore = 0 };
    }
        #endregion

    }
}
