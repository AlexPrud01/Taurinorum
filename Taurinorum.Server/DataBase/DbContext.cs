using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Taurinorum.Object;
using Taurinorum.Object.DataTrasportObject;
using Taurinorum.Object.DataTrasportObject.EmissioneFoglio;
using Taurinorum.Object.FilterModel;
using Taurinorum.Object.FilterModel.EmissioneFoglio;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Taurinorum.Server.DataBase
{
    public class DbContext : BaseDataAccess
    {

        public DbContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        #region <<UTENTE>>
        public async Task<List<UtenteDTO>> Utente_GetListAsync(UtenteFM? filtri)
        {
            List<UtenteDTO> list = new List<UtenteDTO>();

            string xmlFiltri = string.Empty;
            if (filtri != null)
                xmlFiltri = filtri.ToXml();

            DataTable? dataTable = await ReadFromSpAsync("usp_Utente_Get", xmlFiltri);
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    list.Add(new UtenteDTO()
                    {
                        ID = Shared.GetIntFromString(row["ID"].ToString()),
                        Admin = Shared.GetBoolFromString(row["Admin"].ToString()),
                        PagaOraria = Shared.GetDecimalFromString(row["PagaOraria"].ToString()),
                        NumeroOreLavorativeSettimanali = Shared.GetIntFromString(row["NumeroOreLavorateSettimanali"].ToString()),
                        NumeroOreLavorativeExtra = Shared.GetIntFromString(row["NumeroOreLavorateExtra"].ToString()),
                        Nome = row["Nome"].ToString(),
                        Cognome = row["Cognome"].ToString(),
                        Username = row["Username"].ToString(),
                        Password = row["Password"].ToString(),
                        TipoContratto = row["TipoContratto"].ToString()
                    });
                }
            }
            return list.OrderBy(x => x.Username).ToList();
        }
        public async Task<UtenteDTO?> Utente_GetByIdAsync(int id)
        {
            UtenteDTO? obj = null;
            UtenteFM filtri = new UtenteFM() { ID = id, Modalita = 1 };
            List<UtenteDTO> result = await Utente_GetListAsync(filtri);
            if (result != null && result.Count > 0)
                obj = result.First();
            return obj;
        }
        public async Task<UtenteDTO> Utente_GetByUserAndPasswordAsync(UtenteFM utenteFM)
        {
            UtenteDTO? obj = null;
            utenteFM.Modalita = 2;
            List<UtenteDTO> result = await Utente_GetListAsync(utenteFM);
            if (result != null && result.Count > 0)
                obj = result.First();
            return obj;
        }
        public async Task<Allert> Utente_SetAsync(UtenteFM Item)
        {
            try
            {
                int returnValue = 0;


                if (!string.IsNullOrEmpty(connectionString))
                {
                    using SqlConnection con = new SqlConnection(connectionString);
                    string sb = "";
                    if (Item != null)
                    {
                        sb = Item.ToXml();
                    }

                    using SqlCommand Cmd = new SqlCommand("usp_Utente_Set", con);
                    Cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter pReturnValue = ReturnValueParameter();
                    Cmd.Parameters.Add(pReturnValue);

                    SqlParameter pReturnMessage = SqlParameter_ReturnMessage();
                    Cmd.Parameters.Add(pReturnMessage);

                    SqlParameter pIDFromTo = SqlParameter_Int("ID", (int)Item.ID, ParameterDirection.InputOutput);
                    Cmd.Parameters.Add(pIDFromTo);

                    Cmd.Parameters.Add(new SqlParameter("@XML", sb.ToString()));
                    con.Open();

                    await Cmd.ExecuteNonQueryAsync();

                    string messaggioRitorno = (string)pReturnMessage.Value;
                    int valoreRitorno = (int)pReturnValue.Value;

                    if (valoreRitorno < 0)
                        return new Allert() { Messaggio = messaggioRitorno, Valore = valoreRitorno };


                    return new Allert() { Messaggio = messaggioRitorno, Valore = valoreRitorno };
                }
                return new Allert() { Messaggio = "Errore generico", Valore = 0 };
            }

            catch (Exception ex)
            {
                return new Allert() { Messaggio = "Errore generico", Valore = 0 };
            }
        }
        public string Utente_DeleteAsync(int IdUtente)
        {
            try
            {
                string messaggioRitorno = string.Empty;
                int valoreRitorno;


                using SqlConnection con = new SqlConnection(connectionString);
                con.Close();

                using SqlCommand Cmd = new SqlCommand("usp_Utente_Del", con);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter pReturnValue = ReturnValueParameter();
                Cmd.Parameters.Add(pReturnValue);

                SqlParameter pReturnMessage = SqlParameter_ReturnMessage();
                Cmd.Parameters.Add(pReturnMessage);

                SqlParameter pIdUtente = SqlParameter_Int("@IDUtente", IdUtente, ParameterDirection.Input);
                Cmd.Parameters.Add(pIdUtente);

                con.Open();

                int ret = Cmd.ExecuteNonQuery();

                messaggioRitorno = (string)pReturnMessage.Value;
                valoreRitorno = (int)pReturnValue.Value;

                if (valoreRitorno < 0)
                    return messaggioRitorno;

                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion

        #region <<TIMBRATURA>>
        public async Task<List<TimbraturaDTO>> Timbratura_GetListAsync(TimbraFM? filtri)
        {
            List<TimbraturaDTO> list = new List<TimbraturaDTO>();

            string xmlFiltri = string.Empty;
            if (filtri != null)
                xmlFiltri = filtri.ToXml();

            DataTable? dataTable = await ReadFromSpAsync("usp_Timbratura_Get", xmlFiltri);
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    TimbraturaDTO timbraDTO = new TimbraturaDTO();

                    timbraDTO.ID = Shared.GetIntFromString(row["ID"].ToString());
                    timbraDTO.IDUtente = Shared.GetIntFromString(row["IDUtente"].ToString());
                    timbraDTO.DataOraEntrata = Shared.GetDateTimeFromString(row["DataOraEntrata"].ToString());
                    timbraDTO.DataOraUscita = Shared.GetDateTimeFromString(row["DataOraUscita"].ToString());
                    timbraDTO.LongitudineEntrata = Shared.GetDecimalFromString(row["LongitudineEntrata"].ToString());
                    timbraDTO.LongitudineUscita = Shared.GetDecimalFromString(row["LongitudineUscita"].ToString());
                    timbraDTO.LatitudineEntrata = Shared.GetDecimalFromString(row["LatitudineEntrata"].ToString());
                    timbraDTO.LatitudineUscita = Shared.GetDecimalFromString(row["LatitudineUscita"].ToString());
                    timbraDTO.TotaleOre = Shared.GetIntFromString(row["TotaleMinuti"].ToString());
                    
                    list.Add(timbraDTO);
                }
            }
            return list.OrderByDescending(x => x.ID).ToList();
        }
        public async Task<TimbraturaDTO?> Timbratura_GetByIdUtenteAsync(int id)
        {
            TimbraturaDTO? obj = null;
            TimbraFM filtri = new TimbraFM() { IDUtente = id, Modalita = 1 };
            List<TimbraturaDTO> result = await Timbratura_GetListAsync(filtri);
            if (result != null && result.Count > 0)
                obj = result.First();
            return obj;
        }
        #endregion

        #region <<TIMBRA>>
        public async Task<List<TimbraDTO>> Timbra_GetListAsync(TimbraFM? filtri)
        {
            List<TimbraDTO> list = new List<TimbraDTO>();

            string xmlFiltri = string.Empty;
            if (filtri != null)
                xmlFiltri = filtri.ToXml();

            DataTable? dataTable = await ReadFromSpAsync("usp_Timbratura_Get", xmlFiltri);
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    TimbraDTO timbraDTO = new TimbraDTO();

                    timbraDTO.ID = Shared.GetIntFromString(row["ID"].ToString());
                    timbraDTO.IDUtente = Shared.GetIntFromString(row["IDUtente"].ToString());
                    if (row["DataOraUscita"].ToString().IsNullOrEmpty())
                    {
                        timbraDTO.EntUsc = true;
                        timbraDTO.DataOra = Shared.GetDateTimeFromString(row["DataOraEntrata"].ToString());
                    }
                    else
                    {
                        timbraDTO.EntUsc = false;
                        timbraDTO.DataOra = Shared.GetDateTimeFromString(row["DataOraUscito"].ToString());
                    }
                    list.Add(timbraDTO);
                }
            }
            return list.OrderByDescending(x => x.ID).ToList();
        }
        public async Task<TimbraDTO?> Timbra_GetByIdUtenteAsync(int id)
        {
            TimbraDTO? obj = null;
            TimbraFM filtri = new TimbraFM() { IDUtente = id, Modalita = 1 };
            List<TimbraDTO> result = await Timbra_GetListAsync(filtri);
            if (result != null && result.Count > 0)
                obj = result.First();
            return obj;
        }
        public async Task<Allert> Timbra_SetAsync(TimbraFM filtro)
        {
            try
            {
                int returnValue = 0;

                if (!string.IsNullOrEmpty(connectionString))
                {
                    using SqlConnection con = new SqlConnection(connectionString);
                    string sb = filtro.ToXml();

                    using SqlCommand Cmd = new SqlCommand("usp_Timbratura_Set", con);
                    Cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter pReturnValue = ReturnValueParameter();
                    Cmd.Parameters.Add(pReturnValue);

                    SqlParameter pReturnMessage = SqlParameter_ReturnMessage();
                    Cmd.Parameters.Add(pReturnMessage);

                    Cmd.Parameters.Add(new SqlParameter("@XML", filtro.ToXml()));
                    con.Open();

                    await Cmd.ExecuteNonQueryAsync();

                    string messaggioRitorno = (string)pReturnMessage.Value;
                    int valoreRitorno = (int)pReturnValue.Value;

                    return new Allert() { Messaggio = messaggioRitorno, Valore = valoreRitorno };
                }
                return new Allert() { Messaggio = "Timbratura non avvenuta contattare l'assistenza", Valore = 0 };
            }

            catch (Exception ex)
            {
                return new Allert() { Messaggio = "Timbratura non avvenuta contattare l'assistenza", Valore = 0 };
            }
        }
        #endregion

        #region <<FORNIORE>>
        public async Task<List<FornitoreDTO>> Fornitore_GetListAsync(FornitoreFM? filtri)
        {
            List<FornitoreDTO> list = new List<FornitoreDTO>();

            string xmlFiltri = string.Empty;
            if (filtri != null)
                xmlFiltri = filtri.ToXml();

            DataTable? dataTable = await ReadFromSpAsync("usp_Fornitore_Get", xmlFiltri);
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    FornitoreDTO FornitoreDTO = new FornitoreDTO();

                    if (row["ID"].ToString() != null && row["ID"].ToString() != "")
                        FornitoreDTO.ID = Shared.GetIntFromString(row["ID"].ToString());

                    if (row["Nome"].ToString() != null && row["Nome"].ToString() != "")
                        FornitoreDTO.Nome = row["Nome"].ToString();

                    if (row["Codice"].ToString() != null && row["Codice"].ToString() != "")
                        FornitoreDTO.Codice = row["Codice"].ToString();

                    if (row["Descrizione"].ToString() != null && row["Descrizione"].ToString() != "")
                        FornitoreDTO.Descrizione = row["Descrizione"].ToString();

                    if (row["IDIndirizzo"].ToString() != null && row["IDIndirizzo"].ToString() != "")
                        FornitoreDTO.IDIndirizzo = Shared.GetIntFromString(row["IDIndirizzo"].ToString());

                    if (row["PartitaIva"].ToString() != null && row["PartitaIva"].ToString() != "")
                        FornitoreDTO.PartitaIva = row["PartitaIva"].ToString();

                    if (row["CodiceFiscale"].ToString() != null && row["CodiceFiscale"].ToString() != "")
                        FornitoreDTO.Descrizione = row["CodiceFiscale"].ToString();

                    if (row["CodiceSDI"].ToString() != null && row["CodiceSDI"].ToString() != "")
                        FornitoreDTO.CodiceSDI = row["CodiceSDI"].ToString();

                    if (row["Indirizzo_Codice"].ToString() != null && row["Indirizzo_Codice"].ToString() != "")
                        FornitoreDTO.Indirizzo_Codice = row["Indirizzo_Codice"].ToString();

                    if (row["Indirizzo_Descrizione"].ToString() != null && row["Indirizzo_Descrizione"].ToString() != "")
                        FornitoreDTO.Indirizzo_Descrizione = row["Indirizzo_Descrizione"].ToString();

                    if (row["Indirizzo_Via"].ToString() != null && row["Indirizzo_Via"].ToString() != "")
                        FornitoreDTO.Indirizzo_Via = row["Indirizzo_Via"].ToString();

                    if (row["Indirizzo_Citta"].ToString() != null && row["Indirizzo_Citta"].ToString() != "")
                        FornitoreDTO.Indirizzo_Citta = row["Indirizzo_Citta"].ToString();
                    if (row["Indirizzo_Cap"].ToString() != null && row["Indirizzo_Cap"].ToString() != "")
                        FornitoreDTO.Indirizzo_Cap = row["Indirizzo_Cap"].ToString();

                    if (row["Indirizzo_Provincia"].ToString() != null && row["Indirizzo_Provincia"].ToString() != "")
                        FornitoreDTO.Indirizzo_Provincia = row["CodiceSDI"].ToString();

                    if (row["Indirizzo_Nazione"].ToString() != null && row["Indirizzo_Nazione"].ToString() != "")
                        FornitoreDTO.Indirizzo_Nazione = row["Indirizzo_Nazione"].ToString();

                    if (row["Indirizzo_Regione"].ToString() != null && row["Indirizzo_Regione"].ToString() != "")
                        FornitoreDTO.Indirizzo_Regione = row["Indirizzo_Regione"].ToString();

                    list.Add(FornitoreDTO);
                }
            }
            return list.OrderByDescending(x => x.ID).ToList();
        }
        public async Task<FornitoreDTO?> Fornitore_GetByIdAsync(int id)
        {
            FornitoreDTO? obj = null;
            FornitoreFM filtri = new FornitoreFM() { ID = id };
            List<FornitoreDTO> result = await Fornitore_GetListAsync(filtri);
            if (result != null && result.Count > 0)
                obj = result.First();
            return obj;
        }
        public async Task<Allert> Fornitore_SetAsync(FornitoreDTO filtro)
        {
            try
            {
                int returnValue = 0;

                if (!string.IsNullOrEmpty(connectionString))
                {
                    using SqlConnection con = new SqlConnection(connectionString);
                    string sb = filtro.ToXml();

                    using SqlCommand Cmd = new SqlCommand("usp_Fornitore_Set", con);
                    Cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter pReturnValue = ReturnValueParameter();
                    Cmd.Parameters.Add(pReturnValue);

                    SqlParameter pReturnMessage = SqlParameter_ReturnMessage();
                    Cmd.Parameters.Add(pReturnMessage);

                    Cmd.Parameters.Add(new SqlParameter("@XML", filtro.ToXml()));
                    con.Open();

                    await Cmd.ExecuteNonQueryAsync();

                    string messaggioRitorno = (string)pReturnMessage.Value;
                    int valoreRitorno = (int)pReturnValue.Value;

                    return new Allert() { Messaggio = messaggioRitorno, Valore = valoreRitorno };
                }
                return new Allert() { Messaggio = "Salvataggio non avvenuto correttamente", Valore = 0 };
            }

            catch (Exception ex)
            {
                return new Allert() { Messaggio = "Salvataggio non avvenuto correttamente", Valore = 0 };
            }
        }
        public async Task<Allert> Fornitore_DeleteAsync(int ID)
        {
            try
            {
                if (!string.IsNullOrEmpty(connectionString))
                {
                    using SqlConnection con = new SqlConnection(connectionString);

                    using SqlCommand Cmd = new SqlCommand("usp_Fornitore_Del", con);
                    Cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter pReturnValue = ReturnValueParameter();
                    Cmd.Parameters.Add(pReturnValue);

                    SqlParameter pReturnMessage = SqlParameter_ReturnMessage();
                    Cmd.Parameters.Add(pReturnMessage);

                    SqlParameter pIDFromTo = SqlParameter_Int("ID", (int)ID, ParameterDirection.Input);
                    Cmd.Parameters.Add(pIDFromTo);

                    con.Open();

                    await Cmd.ExecuteNonQueryAsync();

                    string messaggioRitorno = (string)pReturnMessage.Value;
                    int valoreRitorno = (int)pReturnValue.Value;

                    return new Allert() { Messaggio = messaggioRitorno, Valore = valoreRitorno };
                }
                return new Allert() { Messaggio = "Eliminazione non avvenuta correttamente", Valore = 0 };
            }

            catch (Exception ex)
            {
                return new Allert() { Messaggio = "Eliminazione non avvenuta correttamente", Valore = 0 };
            }
        }
        #endregion

        #region <<CLIENTE>>
        public async Task<List<ClienteDTO>> Cliente_GetListAsync(ClienteFM? filtri)
        {
            List<ClienteDTO> list = new List<ClienteDTO>();

            string xmlFiltri = string.Empty;
            if (filtri != null)
                xmlFiltri = filtri.ToXml();

            DataTable? dataTable = await ReadFromSpAsync("usp_Cliente_Get", xmlFiltri);
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    ClienteDTO ClienteDTO = new ClienteDTO();

                    if (row["ID"].ToString() != null && row["ID"].ToString() != "")
                        ClienteDTO.ID = Shared.GetIntFromString(row["ID"].ToString());

                    if (row["Codice"].ToString() != null && row["Codice"].ToString() != "")
                        ClienteDTO.Codice = row["Codice"].ToString();

                    if (row["Descrizione"].ToString() != null && row["Descrizione"].ToString() != "")
                        ClienteDTO.Descrizione = row["Descrizione"].ToString();

                    if (row["IDIndirizzo"].ToString() != null && row["IDIndirizzo"].ToString() != "")
                        ClienteDTO.IDIndirizzo = Shared.GetIntFromString(row["IDIndirizzo"].ToString());

                    if (row["Nome"].ToString() != null && row["Nome"].ToString() != "")
                        ClienteDTO.Nome = row["Nome"].ToString();

                    if (row["Cognome"].ToString() != null && row["Cognome"].ToString() != "")
                        ClienteDTO.Cognome = row["Cognome"].ToString();

                    if (row["Denominazione"].ToString() != null && row["Denominazione"].ToString() != "")
                        ClienteDTO.Denominazione = row["Denominazione"].ToString();

                    if (row["Indirizzo_Codice"].ToString() != null && row["Indirizzo_Codice"].ToString() != "")
                        ClienteDTO.Indirizzo_Codice = row["Indirizzo_Codice"].ToString();

                    if (row["Indirizzo_Descrizione"].ToString() != null && row["Indirizzo_Descrizione"].ToString() != "")
                        ClienteDTO.Indirizzo_Descrizione = row["Indirizzo_Descrizione"].ToString();

                    if (row["Indirizzo_Via"].ToString() != null && row["Indirizzo_Via"].ToString() != "")
                        ClienteDTO.Indirizzo_Via = row["Indirizzo_Via"].ToString();

                    if (row["Indirizzo_Citta"].ToString() != null && row["Indirizzo_Citta"].ToString() != "")
                        ClienteDTO.Indirizzo_Citta = row["Indirizzo_Citta"].ToString();
                    if (row["Indirizzo_Cap"].ToString() != null && row["Indirizzo_Cap"].ToString() != "")
                        ClienteDTO.Indirizzo_Cap = row["Indirizzo_Cap"].ToString();

                    if (row["Indirizzo_Provincia"].ToString() != null && row["Indirizzo_Provincia"].ToString() != "")
                        ClienteDTO.Indirizzo_Provincia = row["CodiceSDI"].ToString();

                    if (row["Indirizzo_Nazione"].ToString() != null && row["Indirizzo_Nazione"].ToString() != "")
                        ClienteDTO.Indirizzo_Nazione = row["Indirizzo_Nazione"].ToString();

                    if (row["Indirizzo_Regione"].ToString() != null && row["Indirizzo_Regione"].ToString() != "")
                        ClienteDTO.Indirizzo_Regione = row["Indirizzo_Regione"].ToString();

                    list.Add(ClienteDTO);
                }
            }
            return list.OrderByDescending(x => x.ID).ToList();
        }
        public async Task<ClienteDTO?> Cliente_GetByIdAsync(int id)
        {
            ClienteDTO? obj = null;
            ClienteFM filtri = new ClienteFM() { ID = id };
            List<ClienteDTO> result = await Cliente_GetListAsync(filtri);
            if (result != null && result.Count > 0)
                obj = result.First();
            return obj;
        }
        public async Task<Allert> Cliente_SetAsync(ClienteDTO filtro)
        {
            try
            {
                int returnValue = 0;

                if (!string.IsNullOrEmpty(connectionString))
                {
                    using SqlConnection con = new SqlConnection(connectionString);
                    string sb = filtro.ToXml();

                    using SqlCommand Cmd = new SqlCommand("usp_Cliente_Set", con);
                    Cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter pReturnValue = ReturnValueParameter();
                    Cmd.Parameters.Add(pReturnValue);

                    SqlParameter pReturnMessage = SqlParameter_ReturnMessage();
                    Cmd.Parameters.Add(pReturnMessage);

                    Cmd.Parameters.Add(new SqlParameter("@XML", filtro.ToXml()));
                    con.Open();

                    await Cmd.ExecuteNonQueryAsync();

                    string messaggioRitorno = (string)pReturnMessage.Value;
                    int valoreRitorno = (int)pReturnValue.Value;

                    return new Allert() { Messaggio = messaggioRitorno, Valore = valoreRitorno };
                }
                return new Allert() { Messaggio = "Salvataggio non avvenuto correttamente", Valore = 0 };
            }

            catch (Exception ex)
            {
                return new Allert() { Messaggio = "Salvataggio non avvenuto correttamente", Valore = 0 };
            }
        }
        public async Task<Allert> Cliente_DeleteAsync(int ID)
        {
            try
            {
                if (!string.IsNullOrEmpty(connectionString))
                {
                    using SqlConnection con = new SqlConnection(connectionString);

                    using SqlCommand Cmd = new SqlCommand("usp_Cliente_Del", con);
                    Cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter pReturnValue = ReturnValueParameter();
                    Cmd.Parameters.Add(pReturnValue);

                    SqlParameter pReturnMessage = SqlParameter_ReturnMessage();
                    Cmd.Parameters.Add(pReturnMessage);

                    SqlParameter pIDFromTo = SqlParameter_Int("ID", (int)ID, ParameterDirection.Input);
                    Cmd.Parameters.Add(pIDFromTo);

                    con.Open();

                    await Cmd.ExecuteNonQueryAsync();

                    string messaggioRitorno = (string)pReturnMessage.Value;
                    int valoreRitorno = (int)pReturnValue.Value;

                    return new Allert() { Messaggio = messaggioRitorno, Valore = valoreRitorno };
                }
                return new Allert() { Messaggio = "Eliminazione non avvenuta correttamente", Valore = 0 };
            }

            catch (Exception ex)
            {
                return new Allert() { Messaggio = "Eliminazione non avvenuta correttamente", Valore = 0 };
            }
        }
        #endregion

        #region <<IVA>>
        public async Task<List<IvaDTO>> Iva_GetListAsync(IvaFM? filtri)
        {
            List<IvaDTO> list = new List<IvaDTO>();

            string xmlFiltri = string.Empty;
            if (filtri != null)
                xmlFiltri = filtri.ToXml();

            DataTable? dataTable = await ReadFromSpAsync("usp_Iva_Get", xmlFiltri);
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    IvaDTO IvaDTO = new IvaDTO();

                    if (row["ID"].ToString() != null && row["ID"].ToString() != "")
                        IvaDTO.ID = Shared.GetIntFromString(row["ID"].ToString());

                    if (row["Codice"].ToString() != null && row["Codice"].ToString() != "")
                        IvaDTO.Codice = row["Codice"].ToString();

                    if (row["Descrizione"].ToString() != null && row["Descrizione"].ToString() != "")
                        IvaDTO.Descrizione = row["Descrizione"].ToString();

                    if (row["Descrizione2"].ToString() != null && row["Descrizione2"].ToString() != "")
                        IvaDTO.Descrizione2 = row["Descrizione2"].ToString();

                    list.Add(IvaDTO);
                }
            }
            return list.OrderByDescending(x => x.ID).ToList();
        }
        public async Task<IvaDTO?> Iva_GetByIdAsync(int id)
        {
            IvaDTO? obj = null;
            IvaFM filtri = new IvaFM() { ID = id };
            List<IvaDTO> result = await Iva_GetListAsync(filtri);
            if (result != null && result.Count > 0)
                obj = result.First();
            return obj;
        }
        public async Task<Allert> Iva_SetAsync(IvaDTO filtro)
        {
            try
            {
                int returnValue = 0;

                if (!string.IsNullOrEmpty(connectionString))
                {
                    using SqlConnection con = new SqlConnection(connectionString);
                    string sb = filtro.ToXml();

                    using SqlCommand Cmd = new SqlCommand("usp_Iva_Set", con);
                    Cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter pReturnValue = ReturnValueParameter();
                    Cmd.Parameters.Add(pReturnValue);

                    SqlParameter pReturnMessage = SqlParameter_ReturnMessage();
                    Cmd.Parameters.Add(pReturnMessage);

                    Cmd.Parameters.Add(new SqlParameter("@XML", filtro.ToXml()));
                    con.Open();

                    await Cmd.ExecuteNonQueryAsync();

                    string messaggioRitorno = (string)pReturnMessage.Value;
                    int valoreRitorno = (int)pReturnValue.Value;

                    return new Allert() { Messaggio = messaggioRitorno, Valore = valoreRitorno };
                }
                return new Allert() { Messaggio = "Salvataggio non avvenuto correttamente", Valore = 0 };
            }

            catch (Exception ex)
            {
                return new Allert() { Messaggio = "Salvataggio non avvenuto correttamente", Valore = 0 };
            }
        }
        public async Task<Allert> Iva_DeleteAsync(int ID)
        {
            try
            {
                if (!string.IsNullOrEmpty(connectionString))
                {
                    using SqlConnection con = new SqlConnection(connectionString);

                    using SqlCommand Cmd = new SqlCommand("usp_Iva_Del", con);
                    Cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter pReturnValue = ReturnValueParameter();
                    Cmd.Parameters.Add(pReturnValue);

                    SqlParameter pReturnMessage = SqlParameter_ReturnMessage();
                    Cmd.Parameters.Add(pReturnMessage);

                    SqlParameter pIDFromTo = SqlParameter_Int("ID", (int)ID, ParameterDirection.Input);
                    Cmd.Parameters.Add(pIDFromTo);

                    con.Open();

                    await Cmd.ExecuteNonQueryAsync();

                    string messaggioRitorno = (string)pReturnMessage.Value;
                    int valoreRitorno = (int)pReturnValue.Value;

                    return new Allert() { Messaggio = messaggioRitorno, Valore = valoreRitorno };
                }
                return new Allert() { Messaggio = "Eliminazione non avvenuta correttamente", Valore = 0 };
            }

            catch (Exception ex)
            {
                return new Allert() { Messaggio = "Eliminazione non avvenuta correttamente", Valore = 0 };
            }
        }
        #endregion

        #region <<MODALITA PAGAMENTO>>
        public async Task<List<ModalitaPagamentoDTO>> ModalitaPagamento_GetListAsync(ModalitaPagamentoFM? filtri)
        {
            List<ModalitaPagamentoDTO> list = new List<ModalitaPagamentoDTO>();

            string xmlFiltri = string.Empty;
            if (filtri != null)
                xmlFiltri = filtri.ToXml();

            DataTable? dataTable = await ReadFromSpAsync("usp_ModalitaPagamento_Get", xmlFiltri);
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    ModalitaPagamentoDTO ModalitaPagamentoDTO = new ModalitaPagamentoDTO();

                    if (row["ID"].ToString() != null && row["ID"].ToString() != "")
                        ModalitaPagamentoDTO.ID = Shared.GetIntFromString(row["ID"].ToString());

                    if (row["Codice"].ToString() != null && row["Codice"].ToString() != "")
                        ModalitaPagamentoDTO.Codice = row["Codice"].ToString();

                    if (row["Descrizione"].ToString() != null && row["Descrizione"].ToString() != "")
                        ModalitaPagamentoDTO.Descrizione = row["Descrizione"].ToString();

                    if (row["Descrizione2"].ToString() != null && row["Descrizione2"].ToString() != "")
                        ModalitaPagamentoDTO.Descrizione2 = row["Descrizione2"].ToString();

                    list.Add(ModalitaPagamentoDTO);
                }
            }
            return list.OrderByDescending(x => x.ID).ToList();
        }
        public async Task<ModalitaPagamentoDTO?> ModalitaPagamento_GetByIdAsync(int id)
        {
            ModalitaPagamentoDTO? obj = null;
            ModalitaPagamentoFM filtri = new ModalitaPagamentoFM() { ID = id };
            List<ModalitaPagamentoDTO> result = await ModalitaPagamento_GetListAsync(filtri);
            if (result != null && result.Count > 0)
                obj = result.First();
            return obj;
        }
        public async Task<Allert> ModalitaPagamento_SetAsync(ModalitaPagamentoDTO filtro)
        {
            try
            {
                int returnValue = 0;

                if (!string.IsNullOrEmpty(connectionString))
                {
                    using SqlConnection con = new SqlConnection(connectionString);
                    string sb = filtro.ToXml();

                    using SqlCommand Cmd = new SqlCommand("usp_ModalitaPagamento_Set", con);
                    Cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter pReturnValue = ReturnValueParameter();
                    Cmd.Parameters.Add(pReturnValue);

                    SqlParameter pReturnMessage = SqlParameter_ReturnMessage();
                    Cmd.Parameters.Add(pReturnMessage);

                    Cmd.Parameters.Add(new SqlParameter("@XML", filtro.ToXml()));
                    con.Open();

                    await Cmd.ExecuteNonQueryAsync();

                    string messaggioRitorno = (string)pReturnMessage.Value;
                    int valoreRitorno = (int)pReturnValue.Value;

                    return new Allert() { Messaggio = messaggioRitorno, Valore = valoreRitorno };
                }
                return new Allert() { Messaggio = "Salvataggio non avvenuto correttamente", Valore = 0 };
            }

            catch (Exception ex)
            {
                return new Allert() { Messaggio = "Salvataggio non avvenuto correttamente", Valore = 0 };
            }
        }
        public async Task<Allert> ModalitaPagamento_DeleteAsync(int ID)
        {
            try
            {
                if (!string.IsNullOrEmpty(connectionString))
                {
                    using SqlConnection con = new SqlConnection(connectionString);

                    using SqlCommand Cmd = new SqlCommand("usp_ModalitaPagamento_Del", con);
                    Cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter pReturnValue = ReturnValueParameter();
                    Cmd.Parameters.Add(pReturnValue);

                    SqlParameter pReturnMessage = SqlParameter_ReturnMessage();
                    Cmd.Parameters.Add(pReturnMessage);

                    SqlParameter pIDFromTo = SqlParameter_Int("ID", (int)ID, ParameterDirection.Input);
                    Cmd.Parameters.Add(pIDFromTo);

                    con.Open();

                    await Cmd.ExecuteNonQueryAsync();

                    string messaggioRitorno = (string)pReturnMessage.Value;
                    int valoreRitorno = (int)pReturnValue.Value;

                    return new Allert() { Messaggio = messaggioRitorno, Valore = valoreRitorno };
                }
                return new Allert() { Messaggio = "Eliminazione non avvenuta correttamente", Valore = 0 };
            }

            catch (Exception ex)
            {
                return new Allert() { Messaggio = "Eliminazione non avvenuta correttamente", Valore = 0 };
            }
        }
        #endregion

        #region <<ATTIVITA>>
        public async Task<List<AttivitàDTO>> Attività_GetListAsync(AttivitàFM? filtri)
        {
            List<AttivitàDTO> list = new List<AttivitàDTO>();

            string xmlFiltri = string.Empty;
            if (filtri != null)
                xmlFiltri = filtri.ToXml();

            DataTable? dataTable = await ReadFromSpAsync("usp_Attivita_Get", xmlFiltri);
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    AttivitàDTO AttivitàDTO = new AttivitàDTO();

                    if (row["ID"].ToString() != null && row["ID"].ToString() != "")
                        AttivitàDTO.ID = Shared.GetIntFromString(row["ID"].ToString());

                    if (row["IDAzienda"].ToString() != null && row["IDAzienda"].ToString() != "")
                        AttivitàDTO.IdAzienda = Shared.GetIntFromString(row["IDAzienda"].ToString());

                    if (row["TempoDaImpiegare"].ToString() != null && row["TempoDaImpiegare"].ToString() != "")
                        AttivitàDTO.TempoDaImpegare = DateTime.Parse(row["TempoDaImpiegare"].ToString());

                    if (row["DataScadenza"].ToString() != null && row["DataScadenza"].ToString() != "")
                        AttivitàDTO.DataScadenza = Shared.GetDateTimeFromString(row["DataScadenza"].ToString());

                    if (row["nPersone"].ToString() != null && row["nPersone"].ToString() != "")
                        AttivitàDTO.nPersone = Shared.GetIntFromString(row["nPersone"].ToString());

                    if (row["Codice"].ToString() != null && row["Codice"].ToString() != "")
                        AttivitàDTO.Codice = row["Codice"].ToString();

                    if (row["Descrizione"].ToString() != null && row["Descrizione"].ToString() != "")
                        AttivitàDTO.Descrizione = row["Descrizione"].ToString();

                    if (row["Lunedi"].ToString() != null && row["Lunedi"].ToString() != "")
                        AttivitàDTO.Lunedi = Shared.GetBoolFromString(row["Lunedi"].ToString());

                    if (row["Martedi"].ToString() != null && row["Martedi"].ToString() != "")
                        AttivitàDTO.Martedi = Shared.GetBoolFromString(row["Martedi"].ToString());

                    if (row["Mercoledi"].ToString() != null && row["Mercoledi"].ToString() != "")
                        AttivitàDTO.Mercoledi = Shared.GetBoolFromString(row["Mercoledi"].ToString());

                    if (row["Giovedi"].ToString() != null && row["Giovedi"].ToString() != "")
                        AttivitàDTO.Giovedi = Shared.GetBoolFromString(row["Giovedi"].ToString());

                    if (row["Venerdi"].ToString() != null && row["Venerdi"].ToString() != "")
                        AttivitàDTO.Venerdi = Shared.GetBoolFromString(row["Venerdi"].ToString());

                    if (row["Sabato"].ToString() != null && row["Sabato"].ToString() != "")
                        AttivitàDTO.Sabato = Shared.GetBoolFromString(row["Sabato"].ToString());

                    if (row["Domenica"].ToString() != null && row["Domenica"].ToString() != "")
                        AttivitàDTO.Domenica = Shared.GetBoolFromString(row["Domenica"].ToString());

                    if (row["Prezzo"].ToString() != null && row["Prezzo"].ToString() != "")
                        AttivitàDTO.Prezzo = Shared.GetIntFromString(row["Prezzo"].ToString());

                    list.Add(AttivitàDTO);
                }
            }
            return list.OrderByDescending(x => x.ID).ToList();
        }
        public async Task<AttivitàDTO?> Attività_GetByIdAsync(int id)
        {
            AttivitàDTO? obj = null;
            AttivitàFM filtri = new AttivitàFM() { ID = id };
            List<AttivitàDTO> result = await Attività_GetListAsync(filtri);
            if (result != null && result.Count > 0)
                obj = result.First();
            return obj;
        }
        public async Task<Allert> Attività_SetAsync(AttivitàDTO filtro)
        {
            try
            {
                int returnValue = 0;

                if (!string.IsNullOrEmpty(connectionString))
                {
                    using SqlConnection con = new SqlConnection(connectionString);
                    string sb = filtro.ToXml();

                    using SqlCommand Cmd = new SqlCommand("usp_Attivita_Set", con);
                    Cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter pReturnValue = ReturnValueParameter();
                    Cmd.Parameters.Add(pReturnValue);

                    SqlParameter pReturnMessage = SqlParameter_ReturnMessage();
                    Cmd.Parameters.Add(pReturnMessage);

                    Cmd.Parameters.Add(new SqlParameter("@XML", filtro.ToXml()));
                    con.Open();

                    await Cmd.ExecuteNonQueryAsync();

                    string messaggioRitorno = (string)pReturnMessage.Value;
                    int valoreRitorno = (int)pReturnValue.Value;

                    return new Allert() { Messaggio = messaggioRitorno, Valore = valoreRitorno };
                }
                return new Allert() { Messaggio = "Salvataggio non avvenuto correttamente", Valore = 0 };
            }

            catch (Exception ex)
            {
                return new Allert() { Messaggio = "Salvataggio non avvenuto correttamente", Valore = 0 };
            }
        }
        public async Task<Allert> Attività_DeleteAsync(int ID)
        {
            try
            {
                if (!string.IsNullOrEmpty(connectionString))
                {
                    using SqlConnection con = new SqlConnection(connectionString);

                    using SqlCommand Cmd = new SqlCommand("usp_Attivita_Del", con);
                    Cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter pReturnValue = ReturnValueParameter();
                    Cmd.Parameters.Add(pReturnValue);

                    SqlParameter pReturnMessage = SqlParameter_ReturnMessage();
                    Cmd.Parameters.Add(pReturnMessage);

                    SqlParameter pIDFromTo = SqlParameter_Int("ID", (int)ID, ParameterDirection.Input);
                    Cmd.Parameters.Add(pIDFromTo);

                    con.Open();

                    await Cmd.ExecuteNonQueryAsync();

                    string messaggioRitorno = (string)pReturnMessage.Value;
                    int valoreRitorno = (int)pReturnValue.Value;

                    return new Allert() { Messaggio = messaggioRitorno, Valore = valoreRitorno };
                }
                return new Allert() { Messaggio = "Eliminazione non avvenuta correttamente", Valore = 0 };
            }

            catch (Exception ex)
            {
                return new Allert() { Messaggio = "Eliminazione non avvenuta correttamente", Valore = 0 };
            }
        }
        #endregion


        #region <<ATTIVITA CEDUTA>>
        public async Task<Allert> AttivitàCeduta_SetAsync(AttivitaCedutaDTO filtro)
        {
            try
            {
                int returnValue = 0;

                if (!string.IsNullOrEmpty(connectionString))
                {
                    using SqlConnection con = new SqlConnection(connectionString);
                    string sb = filtro.ToXml();

                    using SqlCommand Cmd = new SqlCommand("usp_AttivitaCeduta_Set", con);
                    Cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter pReturnValue = ReturnValueParameter();
                    Cmd.Parameters.Add(pReturnValue);

                    SqlParameter pReturnMessage = SqlParameter_ReturnMessage();
                    Cmd.Parameters.Add(pReturnMessage);

                    Cmd.Parameters.Add(new SqlParameter("@XML", filtro.ToXml()));
                    con.Open();

                    await Cmd.ExecuteNonQueryAsync();

                    string messaggioRitorno = (string)pReturnMessage.Value;
                    int valoreRitorno = (int)pReturnValue.Value;

                    return new Allert() { Messaggio = messaggioRitorno, Valore = valoreRitorno };
                }
                return new Allert() { Messaggio = "Salvataggio non avvenuto correttamente", Valore = 0 };
            }

            catch (Exception ex)
            {
                return new Allert() { Messaggio = "Salvataggio non avvenuto correttamente", Valore = 0 };
            }
        }

        #endregion

        #region <<COMPILAZIONE ORE>>

        public async Task<List<CompilazioneOreDTO>> CompilazioneOre_GetListAsync(CompilazioneOreFM? filtri)
        {
            List<CompilazioneOreDTO> list = new List<CompilazioneOreDTO>();

            string xmlFiltri = string.Empty;
            if (filtri != null)
                xmlFiltri = filtri.ToXml();

            DataTable? dataTable = await ReadFromSpAsync("usp_CompilazioneOre_Get", xmlFiltri);
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    list.Add(new CompilazioneOreDTO()
                    {
                        ID = Shared.GetIntFromString(row["ID"].ToString()),
                        IdUtente = Shared.GetIntFromString(row["IDUtente"].ToString()),
                        Azienda = row["Azienda"].ToString(),
                        Attività = row["Attivita"].ToString(),
                        DataOraInizio = Shared.GetDateTimeFromString(row["DataOraInizio"].ToString()),
                        DataOraFine = Shared.GetDateTimeFromString(row["DataOraFine"].ToString()),
                        TotaleMinuti = Shared.GetIntFromString(row["TotaleMinuti"].ToString())
                    });
                }
            }
            return list.OrderBy(x => x.DataOraInizio).ToList();
        }
        public async Task<CompilazioneOreDTO> CompilazioneOre_GetByIdAsync(int ID)
        {
            CompilazioneOreDTO? obj = null;
            CompilazioneOreFM filtri = new CompilazioneOreFM() { ID = ID, Modalita = 1 };
            List<CompilazioneOreDTO> result = await CompilazioneOre_GetListAsync(filtri);
            if (result != null && result.Count > 0)
                obj = result.First();
            return obj;
        }
        public async Task<Allert> CompilazioneOre_SetAsync(CompilazioneOreFM filtri = null)
        {
            try
            {
                if (!string.IsNullOrEmpty(connectionString))
                {
                    using SqlConnection con = new SqlConnection(connectionString);
                    string sb = "";

                    if (filtri != null)
                        sb = filtri.ToXml();

                    using SqlCommand Cmd = new SqlCommand("usp_CompilazioneOre_Set", con);
                    Cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter pReturnValue = ReturnValueParameter();
                    Cmd.Parameters.Add(pReturnValue);

                    SqlParameter pReturnMessage = SqlParameter_ReturnMessage();
                    Cmd.Parameters.Add(pReturnMessage);

                    Cmd.Parameters.Add(new SqlParameter("@XML", sb.ToString()));
                    con.Open();

                    await Cmd.ExecuteNonQueryAsync();

                    string messaggioRitorno = (string)pReturnMessage.Value;
                    int valoreRitorno = (int)pReturnValue.Value;

                    if (valoreRitorno < 0)
                        return new Allert() { Messaggio = messaggioRitorno, Valore = valoreRitorno };

                    return new Allert() { Messaggio = messaggioRitorno, Valore = valoreRitorno };
                }

                return new Allert() { Messaggio = "Errore generico", Valore = 0 };
            }
            catch (Exception ex)
            {
                return new Allert() { Messaggio = "Errore generico", Valore = 0 };
            }
        }
        public async Task<Allert> CompilazioneOre_DeleteAsync(int ID)
        {
            try
            {
                if (!string.IsNullOrEmpty(connectionString))
                {
                    using SqlConnection con = new SqlConnection(connectionString);

                    using SqlCommand Cmd = new SqlCommand("usp_CompilazioneOre_Del", con);
                    Cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter pReturnValue = ReturnValueParameter();
                    Cmd.Parameters.Add(pReturnValue);

                    SqlParameter pReturnMessage = SqlParameter_ReturnMessage();
                    Cmd.Parameters.Add(pReturnMessage);

                    SqlParameter pIDFromTo = SqlParameter_Int("ID", (int)ID, ParameterDirection.InputOutput);
                    Cmd.Parameters.Add(pIDFromTo);

                    con.Open();

                    await Cmd.ExecuteNonQueryAsync();

                    string messaggioRitorno = (string)pReturnMessage.Value;
                    int valoreRitorno = (int)pReturnValue.Value;

                    return new Allert() { Messaggio = messaggioRitorno, Valore = valoreRitorno };
                }
                return new Allert() { Messaggio = "Eliminazione non avvenuta correttamente", Valore = 0 };
            }

            catch (Exception ex)
            {
                return new Allert() { Messaggio = "Eliminazione non avvenuta correttamente", Valore = 0 };
            }
        }
        #endregion
    }
}
