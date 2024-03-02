using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Taurinorum.Object;
using Taurinorum.Object.DataTrasportObject;
using Taurinorum.Object.DataTrasportObject.EmissioneFoglio;
using Taurinorum.Object.FilterModel;
using Taurinorum.Object.FilterModel.EmissioneFoglio;

namespace Taurinorum.Center
{
    public class RestService
    {
        private HttpClient _client; 
        string BaseAddress = "http://172.20.10.2:3000/";
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
        public async Task<List<UtenteDTO>> Utente_GetListAsync(UtenteFM? timbraFM)
        {
            List<TimbraDTO> listItem = new List<TimbraDTO>();

            string json = JsonConvert.SerializeObject(timbraFM);
            StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");


            Uri uri = new Uri(string.Format(BaseAddress + "Service/UtenteGetList"));
            HttpResponseMessage response;
            try
            {

                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = uri,
                    Content = httpContent,
                };

                response = await _client.SendAsync(request);

                //response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    List<UtenteDTO> listUtente = JsonConvert.DeserializeObject<List<UtenteDTO>>(content);
                    return listUtente;
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return new List<UtenteDTO>();
            }
            return new List<UtenteDTO>();
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
                    return JsonConvert.DeserializeObject<UtenteDTO>(content);
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
        public async Task<Allert> Utente_SetAsync(UtenteFM Item)
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
                    var obj = JsonConvert.DeserializeObject<Allert>(content);
                    return obj;
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return new Allert() { Messaggio = "errore", Valore = 0 };
            }
            return new Allert() { Messaggio = "errore", Valore = 0 };
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

        #region <<COMPILAZIONEORE>>

        public async Task<List<CompilazioneOreDTO>> CompilazioneOre_GetList(CompilazioneOreFM compilazioneOreFm)
        {
            string json = JsonConvert.SerializeObject(compilazioneOreFm);
            StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            Uri uri = new Uri(string.Format(BaseAddress + "Service/CompilazioneOreGetList"));
            HttpResponseMessage response;
            try
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = uri,
                    Content = httpContent,
                };
                response = await _client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<CompilazioneOreDTO>>(content);
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return new List<CompilazioneOreDTO>();
            }
            return new List<CompilazioneOreDTO>();
        }
        public async Task<CompilazioneOreDTO?> CompilazioneOre_GetByIdAsync(int id)
        {
            Uri uri = new Uri(string.Format(BaseAddress + "Service/CompilazioneOreGetById/{0}", id));
            HttpResponseMessage response;
            try
            {
                response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<CompilazioneOreDTO>(content);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new CompilazioneOreDTO();
            }
            return new CompilazioneOreDTO();
        }
        public async Task<Allert> CompilazioneOre_SetAsync(CompilazioneOreFM Item)
        {
            string json = JsonConvert.SerializeObject(Item);
            StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            Uri uri = new Uri(string.Format(BaseAddress + "Service/CompilazioneOreSet"));
            HttpResponseMessage response;
            try
            {
                response = await _client.PostAsync(uri, httpContent);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    var obj = JsonConvert.DeserializeObject<Allert>(content);
                    return obj;
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return new Allert() { Messaggio = "Errore", Valore = 0 };
            }
            return new Allert() { Messaggio = "Errore", Valore = 0 };
        }

        public async Task<string> CompilazioneOre_DeleteAsync(int IdAzienda)
        {
            Uri uri = new Uri(string.Format(BaseAddress + "Service/CompilazioneOreDelete/{0}", IdAzienda));
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

        #region <<AZIENDA>>
        public async Task<List<AziendaDTO>> Azienda_GetListAsync(AziendaFM? AziendaFM)
        {
            List<AziendaDTO> listItem = new List<AziendaDTO>();

            string json = JsonConvert.SerializeObject(AziendaFM);
            StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");


            Uri uri = new Uri(string.Format(BaseAddress + "Service/AziendaGetList"));
            HttpResponseMessage response;
            try
            {

                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = uri,
                    Content = httpContent,
                };

                response = await _client.SendAsync(request);

                //response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<AziendaDTO>>(content);
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return new List<AziendaDTO>();
            }
            return new List<AziendaDTO>();
        }

        public async Task<AziendaDTO?> Azienda_GetByIdAsync(int id)
        {
            Uri uri = new Uri(string.Format(BaseAddress + "Service/AziendaGetByID/{0}", id));
            HttpResponseMessage response;
            try
            {
                response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<AziendaDTO>(content);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new AziendaDTO();
            }
            return new AziendaDTO();
        }
        public async Task<AziendaDTO> Azienda_GetByUserAndPasswordAsync(AziendaFM? AziendaFM)
        {
            AziendaDTO listItem = new AziendaDTO();

            string json = JsonConvert.SerializeObject(AziendaFM);
            StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");


            Uri uri = new Uri(string.Format(BaseAddress + "Service/AziendaGetUserPassword"));
            HttpResponseMessage response;
            try
            {

                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = uri,
                    Content = httpContent,
                };

                response = await _client.SendAsync(request);

                //response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<AziendaDTO>(content);
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return new AziendaDTO();
            }
            return new AziendaDTO();
        }

        public async Task<Allert> Azienda_SetAsync(AziendaDTO Item)
        {
            string json = JsonConvert.SerializeObject(Item);
            StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            Uri uri = new Uri(string.Format(BaseAddress + "Service/AziendaSet"));
            HttpResponseMessage response;
            try
            {
                response = await _client.PostAsync(uri, httpContent);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    var obj = JsonConvert.DeserializeObject<Allert>(content);
                    return obj;
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return new Allert() { Messaggio = "Errore", Valore = 0 };
            }
            return new Allert() { Messaggio = "Errore", Valore = 0 };
        }

        public async Task<string> Azienda_DeleteAsync(int IdAzienda)
        {
            Uri uri = new Uri(string.Format(BaseAddress + "Service/AziendaDel/{0}", IdAzienda));
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

        #region <<UTENTEGIORNOATTIVITA>>
        public async Task<List<UtenteGiornoAttivitàDTO>> UtenteGiornoAttività_GetListAsync(UtenteGiornoAttivitàFM? utenteFM)
        {
            List<UtenteGiornoAttivitàDTO> listItem = new List<UtenteGiornoAttivitàDTO>();

            string json = JsonConvert.SerializeObject(utenteFM);
            StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");


            Uri uri = new Uri(string.Format(BaseAddress + "Service/UtenteGiornoAttività_GetListAsync"));
            HttpResponseMessage response;
            try
            {

                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = uri,
                    Content = httpContent,
                };

                response = await _client.SendAsync(request);

                //response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<UtenteGiornoAttivitàDTO>>(content);
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return new List<UtenteGiornoAttivitàDTO>();
            }
            return new List<UtenteGiornoAttivitàDTO>();
        }
        public async Task<UtenteGiornoAttivitàDTO?> UtenteGiornoAttività_GetByIdAsync(int id)
        {
            Uri uri = new Uri(string.Format(BaseAddress + "Service/UtenteGiornoAttivita_GetByIdAsync/{0}", id));
            HttpResponseMessage response;
            try
            {
                response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<UtenteGiornoAttivitàDTO>(content);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new UtenteGiornoAttivitàDTO();
            }
            return new UtenteGiornoAttivitàDTO();
        }
        public async Task<List<UtenteGiornoAttivitàDTO>> UtenteGiornoAttività_List_GetByIDAttivita(int IDAttivita)
        {
            List<UtenteGiornoAttivitàDTO> listItem = new List<UtenteGiornoAttivitàDTO>();

            Uri uri = new Uri(string.Format(BaseAddress + "Service/UtenteGiornoAttività_List_GetByIDAttivita/" + IDAttivita));
            HttpResponseMessage response;
            try
            {
                response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<UtenteGiornoAttivitàDTO>>(content);
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return new List<UtenteGiornoAttivitàDTO>();
            }
            return new List<UtenteGiornoAttivitàDTO>();
        }
        public async Task<List<UtenteGiornoAttivitàDTO>> UtenteGiornoAttività_List_GetByIDUtente(int IDUtente)
        {
            List<UtenteGiornoAttivitàDTO> listItem = new List<UtenteGiornoAttivitàDTO>();

            Uri uri = new Uri(string.Format(BaseAddress + "Service/UtenteGiornoAttività_List_GetByIDUtente/" + IDUtente));
            HttpResponseMessage response;
            try
            {
                response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<UtenteGiornoAttivitàDTO>>(content);
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return new List<UtenteGiornoAttivitàDTO>();
            }
            return new List<UtenteGiornoAttivitàDTO>();
        }
        public async Task<UtenteGiornoAttivitàDTO> UtenteGiornoAttività_GetByIDAttivitaAndIDUtente(int IDUtente, int IDAttivita)
        {
            List<UtenteGiornoAttivitàDTO> listItem = new List<UtenteGiornoAttivitàDTO>();

            Uri uri = new Uri(string.Format(BaseAddress + "Service/UtenteGiornoAttività_List_GetByIDUtente/{0}/{1}",IDUtente, IDAttivita));
            HttpResponseMessage response;
            try
            {
                response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<UtenteGiornoAttivitàDTO>(content);
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return new UtenteGiornoAttivitàDTO();
            }
            return new UtenteGiornoAttivitàDTO();
        }
        public async Task<Allert> UtenteGiornoAttività_SetAsync(UtenteGiornoAttivitàDTO Item)
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
                    var obj = JsonConvert.DeserializeObject<Allert>(content);
                    return obj;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new Allert() { Messaggio = "Errore", Valore = 0 };
            }
            return new Allert() { Messaggio = "Errore", Valore = 0 };
        }

        public async Task<string> UtenteGiornoAttività_DeleteAsync(int IdUtente)
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

        #region <<FORNITORE>>

        public async Task<List<FornitoreDTO>> Fornitore_GetListAsync(FornitoreFM? timbraFM)
        {
            string json = JsonConvert.SerializeObject(timbraFM);
            StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");


            Uri uri = new Uri(string.Format(BaseAddress + "Service/FornitoreGetList"));
            HttpResponseMessage response;
            try
            {

                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = uri,
                    Content = httpContent,
                };

                response = await _client.SendAsync(request);

                //response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    List<FornitoreDTO> listAttività = JsonConvert.DeserializeObject<List<FornitoreDTO>>(content);
                    return listAttività;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new List<FornitoreDTO>();
            }
            return new List<FornitoreDTO>();
        }
        public async Task<FornitoreDTO?> Fornitore_GetByIdAsync(int id)
        {
            Uri uri = new Uri(string.Format(BaseAddress + "Service/FornitoreGetByID/{0}", id));
            HttpResponseMessage response;
            try
            {
                response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<FornitoreDTO>(content);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            return null;
        }
        public async Task<Allert> Fornitore_SetAsync(FornitoreDTO Item)
        {
            string json = JsonConvert.SerializeObject(Item);
            StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            Uri uri = new Uri(string.Format(BaseAddress + "Service/FornitoreSet"));
            HttpResponseMessage response;
            try
            {
                response = await _client.PostAsync(uri, httpContent);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    var obj = JsonConvert.DeserializeObject<Allert>(content);
                    return obj;
                }
            }
            catch (Exception e)
            {
                return new Allert() { Messaggio = "errore", Valore = 0 };
            }
            return new Allert() { Messaggio = "errore", Valore = 0 };
        }
        public async Task<Allert> Fornitore_DeleteAsync(int IdFornitore)
        {
            Uri uri = new Uri(string.Format(BaseAddress + "Service/FornitoreDel/{0}", IdFornitore));
            HttpResponseMessage response;
            try
            {
                response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Allert>(content);
                }
            }
            catch (Exception e)
            {
                return new Allert() { Messaggio = "errore", Valore = 0 };
            }
            return new Allert() { Messaggio = "errore", Valore = 0 };
        }
        #endregion

        #region <<IVA>>

        public async Task<List<IvaDTO>> Iva_GetListAsync(IvaFM? timbraFM)
        {
            string json = JsonConvert.SerializeObject(timbraFM);
            StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");


            Uri uri = new Uri(string.Format(BaseAddress + "Service/IvaGetList"));
            HttpResponseMessage response;
            try
            {

                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = uri,
                    Content = httpContent,
                };

                response = await _client.SendAsync(request);

                //response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    List<IvaDTO> listAttività = JsonConvert.DeserializeObject<List<IvaDTO>>(content);
                    return listAttività;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new List<IvaDTO>();
            }
            return new List<IvaDTO>();
        }
        public async Task<IvaDTO?> Iva_GetByIdAsync(int id)
        {
            Uri uri = new Uri(string.Format(BaseAddress + "Service/IvaGetByID/{0}", id));
            HttpResponseMessage response;
            try
            {
                response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<IvaDTO>(content);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            return null;
        }
        public async Task<Allert> Iva_SetAsync(IvaDTO Item)
        {
            string json = JsonConvert.SerializeObject(Item);
            StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            Uri uri = new Uri(string.Format(BaseAddress + "Service/IvaSet"));
            HttpResponseMessage response;
            try
            {
                response = await _client.PostAsync(uri, httpContent);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    var obj = JsonConvert.DeserializeObject<Allert>(content);
                    return obj;
                }
            }
            catch (Exception e)
            {
                return new Allert() { Messaggio = "errore", Valore = 0 };
            }
            return new Allert() { Messaggio = "errore", Valore = 0 };
        }
        public async Task<Allert> Iva_DeleteAsync(int IdIva)
        {
            Uri uri = new Uri(string.Format(BaseAddress + "Service/IvaDel/{0}", IdIva));
            HttpResponseMessage response;
            try
            {
                response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Allert>(content);
                }
            }
            catch (Exception e)
            {
                return new Allert() { Messaggio = "errore", Valore = 0 };
            }
            return new Allert() { Messaggio = "errore", Valore = 0 };
        }
        #endregion

        #region <<MODALITA PAGAMENTO>>

        public async Task<List<ModalitaPagamentoDTO>> ModalitaPagamento_GetListAsync(ModalitaPagamentoFM? modalitaPagamentoFM)
        {
            string json = JsonConvert.SerializeObject(modalitaPagamentoFM);
            StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");


            Uri uri = new Uri(string.Format(BaseAddress + "Service/ModalitaPagamentoGetList"));
            HttpResponseMessage response;
            try
            {

                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = uri,
                    Content = httpContent,
                };

                response = await _client.SendAsync(request);

                //response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    List<ModalitaPagamentoDTO> listAttività = JsonConvert.DeserializeObject<List<ModalitaPagamentoDTO>>(content);
                    return listAttività;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new List<ModalitaPagamentoDTO>();
            }
            return new List<ModalitaPagamentoDTO>();
        }
        public async Task<ModalitaPagamentoDTO?> ModalitaPagamento_GetByIdAsync(int id)
        {
            Uri uri = new Uri(string.Format(BaseAddress + "Service/ModalitaPagamentoGetByID/{0}", id));
            HttpResponseMessage response;
            try
            {
                response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ModalitaPagamentoDTO>(content);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            return null;
        }
        public async Task<Allert> ModalitaPagamento_SetAsync(ModalitaPagamentoDTO Item)
        {
            string json = JsonConvert.SerializeObject(Item);
            StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            Uri uri = new Uri(string.Format(BaseAddress + "Service/ModalitaPagamentoSet"));
            HttpResponseMessage response;
            try
            {
                response = await _client.PostAsync(uri, httpContent);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    var obj = JsonConvert.DeserializeObject<Allert>(content);
                    return obj;
                }
            }
            catch (Exception e)
            {
                return new Allert() { Messaggio = "errore", Valore = 0 };
            }
            return new Allert() { Messaggio = "errore", Valore = 0 };
        }
        public async Task<Allert> ModalitaPagamento_DeleteAsync(int IdModalitaPagamento)
        {
            Uri uri = new Uri(string.Format(BaseAddress + "Service/ModalitaPagamentoDel/{0}", IdModalitaPagamento));
            HttpResponseMessage response;
            try
            {
                response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Allert>(content);
                }
            }
            catch (Exception e)
            {
                return new Allert() { Messaggio = "errore", Valore = 0 };
            }
            return new Allert() { Messaggio = "errore", Valore = 0 };
        }
        #endregion

        #region <<CLIENTE>>

        public async Task<List<ClienteDTO>> Cliente_GetListAsync(ClienteFM? timbraFM)
        {
            string json = JsonConvert.SerializeObject(timbraFM);
            StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");


            Uri uri = new Uri(string.Format(BaseAddress + "Service/ClienteGetList"));
            HttpResponseMessage response;
            try
            {

                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = uri,
                    Content = httpContent,
                };

                response = await _client.SendAsync(request);

                //response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    List<ClienteDTO> listAttività = JsonConvert.DeserializeObject<List<ClienteDTO>>(content);
                    return listAttività;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new List<ClienteDTO>();
            }
            return new List<ClienteDTO>();
        }
        public async Task<ClienteDTO?> Cliente_GetByIdAsync(int id)
        {
            Uri uri = new Uri(string.Format(BaseAddress + "Service/ClienteGetByID/{0}", id));
            HttpResponseMessage response;
            try
            {
                response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ClienteDTO>(content);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            return null;
        }
        public async Task<Allert> Cliente_SetAsync(ClienteDTO Item)
        {
            string json = JsonConvert.SerializeObject(Item);
            StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            Uri uri = new Uri(string.Format(BaseAddress + "Service/ClienteSet"));
            HttpResponseMessage response;
            try
            {
                response = await _client.PostAsync(uri, httpContent);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    var obj = JsonConvert.DeserializeObject<Allert>(content);
                    return obj;
                }
            }
            catch (Exception e)
            {
                return new Allert() { Messaggio = "errore", Valore = 0 };
            }
            return new Allert() { Messaggio = "errore", Valore = 0 };
        }
        public async Task<Allert> Cliente_DeleteAsync(int IdCliente)
        {
            Uri uri = new Uri(string.Format(BaseAddress + "Service/ClienteDel/{0}", IdCliente));
            HttpResponseMessage response;
            try
            {
                response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Allert>(content);
                }
            }
            catch (Exception e)
            {
                return new Allert() { Messaggio = "errore", Valore = 0 };
            }
            return new Allert() { Messaggio = "errore", Valore = 0 };
        }
        #endregion

        #region <<ATTIVITA>>

        public async Task<List<AttivitàDTO>> Attività_GetListAsync(AttivitàFM? timbraFM)
        {
            string json = JsonConvert.SerializeObject(timbraFM);
            StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");


            Uri uri = new Uri(string.Format(BaseAddress + "Service/AttivitaGetList"));
            HttpResponseMessage response;
            try
            {

                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = uri,
                    Content = httpContent,
                };

                response = await _client.SendAsync(request);

                //response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    List<AttivitàDTO> listAttività = JsonConvert.DeserializeObject<List<AttivitàDTO>>(content);
                    return listAttività;
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return new List<AttivitàDTO>();
            }
            return new List<AttivitàDTO>();
        }
        public async Task<AttivitàDTO?> Attività_GetByIdAsync(int id)
        {
            Uri uri = new Uri(string.Format(BaseAddress + "Service/AttivitaGetByID/{0}", id));
            HttpResponseMessage response;
            try
            {
                response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<AttivitàDTO>(content);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            return null;
        }
        public async Task<Allert> Attività_SetAsync(AttivitàDTO Item)
        {
            string json = JsonConvert.SerializeObject(Item);
            StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            Uri uri = new Uri(string.Format(BaseAddress + "Service/AttivitaSet"));
            HttpResponseMessage response;
            try
            {
                response = await _client.PostAsync(uri, httpContent);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    var obj = JsonConvert.DeserializeObject<Allert>(content);
                    return obj;
                }
            }
            catch (Exception e)
            {
                return new Allert() { Messaggio = "errore", Valore = 0 };
            }
            return new Allert() { Messaggio = "errore", Valore = 0 };
        }
        public async Task<Allert> Attività_DeleteAsync(int IdAttività)
        {
            Uri uri = new Uri(string.Format(BaseAddress + "Service/AttivitaDel/{0}", IdAttività));
            HttpResponseMessage response;
            try
            {
                response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Allert>(content);
                }
            }
            catch (Exception e)
            {
                return new Allert() { Messaggio = "errore", Valore = 0 };
            }
            return new Allert() { Messaggio = "errore", Valore = 0 };
        }
        #endregion


        #region <<ATTIVITA CEDUTA>>
        public async Task<Allert> AttivitàCeduta_SetAsync(AttivitaCedutaDTO Item)
        {
            string json = JsonConvert.SerializeObject(Item);
            StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            Uri uri = new Uri(string.Format(BaseAddress + "Service/AttivitaCedutaSet"));
            HttpResponseMessage response;
            try
            {
                response = await _client.PostAsync(uri, httpContent);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    var obj = JsonConvert.DeserializeObject<Allert>(content);
                    return obj;
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

        #region <<TIMBRATRICE>>
        public async Task<List<TimbraturaDTO>> Timbratura_GetListAsync(TimbraFM? timbraFM)
        {
            List<TimbraDTO> listItem = new List<TimbraDTO>();

            string json = JsonConvert.SerializeObject(timbraFM);
            StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");


            Uri uri = new Uri(string.Format(BaseAddress + "Service/TimbraturaGetList"));
            HttpResponseMessage response;
            try
            {

                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = uri,
                    Content = httpContent,
                };

                response = await _client.SendAsync(request);

                //response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<TimbraturaDTO>>(content);
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return new List<TimbraturaDTO>();
            }
            return new List<TimbraturaDTO>();
        }
        #endregion
    }
}