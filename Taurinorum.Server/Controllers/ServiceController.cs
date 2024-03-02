using Microsoft.AspNetCore.Mvc;
using Taurinorum.Object;
using Taurinorum.Object.DataTrasportObject;
using Taurinorum.Object.DataTrasportObject.EmissioneFoglio;
using Taurinorum.Object.FilterModel;
using Taurinorum.Object.FilterModel.EmissioneFoglio;
using Taurinorum.Server.DataBase;

namespace Taurinorum.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServiceController : ControllerBase
    {
        private DbContext context;
        private readonly ILogger<ServiceController> _logger;

        public ServiceController(ILogger<ServiceController> logger, DbContext dbContext)
        {
            _logger = logger;
            this.context = dbContext;
        }

        #region <<TIMBRA>>

        [HttpGet("TimbraturaGetList")]
        public async Task<List<TimbraturaDTO>> Timbratura_GetListAsync([FromBody] TimbraFM filtri = null)
        {
            return await context.Timbratura_GetListAsync(filtri);
        }
        [HttpGet("TimbraGetList")]
        public async Task<List<TimbraDTO>> Timbra_GetListAsync([FromBody] TimbraFM filtri = null)
        {
            return await context.Timbra_GetListAsync(filtri);
        }

        [HttpPost("Timbra")]
        public async Task<Allert> Timbra_SetAsync([FromBody] TimbraFM filtri = null)
        {
            return await context.Timbra_SetAsync(filtri);
        }
        [HttpGet("UltimaTimbratura/{ID}")]
        public async Task<TimbraDTO> Timbra_GetByIdUtenteAsync(int ID)
        {
            return await context.Timbra_GetByIdUtenteAsync(ID);
        }


        [HttpGet("TimbraturaGetByIdUtente/{IDUtente}")]
        public async Task<TimbraDTO> Timbra_GetByIdAsync(int IDUtente)
        {
            return await context.Timbra_GetByIdUtenteAsync(IDUtente);
        }

        #endregion


        #region <<FORNITORE>>

        [HttpGet("FornitoreGetList")]
        public async Task<List<FornitoreDTO>> Fornitore_GetList([FromBody] FornitoreFM filtri = null)
        {
            var obj = await context.Fornitore_GetListAsync(filtri);
            return obj;
        }

        [HttpGet("FornitoreGetByID/{ID}")]
        public async Task<FornitoreDTO> Fornitore_GetByIdAsync(int ID)
        {
            return await context.Fornitore_GetByIdAsync(ID);
        }

        [HttpPost("FornitoreSet")]
        public async Task<Allert> Fornitore_SetAsync([FromBody] FornitoreDTO filtri = null)
        {
            return await context.Fornitore_SetAsync(filtri);
        }
        [HttpGet("FornitoreDel/{ID}")]
        public async Task<Allert> Fornitore_DeleteAsync(int ID)
        {
            return await context.Fornitore_DeleteAsync(ID);
        }
        #endregion

        #region <<CLIENTE>>

        [HttpGet("ClienteGetList")]
        public async Task<List<ClienteDTO>> Cliente_GetList([FromBody] ClienteFM filtri = null)
        {
            var obj = await context.Cliente_GetListAsync(filtri);
            return obj;
        }

        [HttpGet("ClienteGetByID/{ID}")]
        public async Task<ClienteDTO> Cliente_GetByIdAsync(int ID)
        {
            return await context.Cliente_GetByIdAsync(ID);
        }

        [HttpPost("ClienteSet")]
        public async Task<Allert> Cliente_SetAsync([FromBody] ClienteDTO filtri = null)
        {
            return await context.Cliente_SetAsync(filtri);
        }
        [HttpGet("ClienteDel/{ID}")]
        public async Task<Allert> Cliente_DeleteAsync(int ID)
        {
            return await context.Cliente_DeleteAsync(ID);
        }
        #endregion

        #region <<IVA>>

        [HttpGet("IvaGetList")]
        public async Task<List<IvaDTO>> Iva_GetList([FromBody] IvaFM filtri = null)
        {
            var obj = await context.Iva_GetListAsync(filtri);
            return obj;
        }

        [HttpGet("IvaGetByID/{ID}")]
        public async Task<IvaDTO> Iva_GetByIdAsync(int ID)
        {
            return await context.Iva_GetByIdAsync(ID);
        }

        [HttpPost("IvaSet")]
        public async Task<Allert> Iva_SetAsync([FromBody] IvaDTO filtri = null)
        {
            return await context.Iva_SetAsync(filtri);
        }
        [HttpGet("IvaDel/{ID}")]
        public async Task<Allert> Iva_DeleteAsync(int ID)
        {
            return await context.Iva_DeleteAsync(ID);
        }
        #endregion

        #region <<MODALITA PAGAMENTO>>

        [HttpGet("ModalitaPagamentoGetList")]
        public async Task<List<ModalitaPagamentoDTO>> ModalitaPagamento_GetList([FromBody] ModalitaPagamentoFM filtri = null)
        {
            var obj = await context.ModalitaPagamento_GetListAsync(filtri);
            return obj;
        }

        [HttpGet("ModalitaPagamentoGetByID/{ID}")]
        public async Task<ModalitaPagamentoDTO> ModalitaPagamento_GetByIdAsync(int ID)
        {
            return await context.ModalitaPagamento_GetByIdAsync(ID);
        }

        [HttpPost("ModalitaPagamentoSet")]
        public async Task<Allert> ModalitaPagamento_SetAsync([FromBody] ModalitaPagamentoDTO filtri = null)
        {
            return await context.ModalitaPagamento_SetAsync(filtri);
        }
        [HttpGet("ModalitaPagamentoDel/{ID}")]
        public async Task<Allert> ModalitaPagamento_DeleteAsync(int ID)
        {
            return await context.ModalitaPagamento_DeleteAsync(ID);
        }
        #endregion

        #region <<ATTIVITA>>

        [HttpGet("AttivitaGetList")]
        public async Task<List<AttivitàDTO>> Attivita_GetList([FromBody] AttivitàFM filtri = null)
        {
            var obj = await context.Attività_GetListAsync(filtri);
            return obj;
        }

        [HttpGet("AttivitaGetByID/{ID}")]
        public async Task<AttivitàDTO> Attivita_GetByIdAsync(int ID)
        {
            return await context.Attività_GetByIdAsync(ID);
        }

        [HttpPost("AttivitaSet")]
        public async Task<Allert> Attivita_SetAsync([FromBody] AttivitàDTO filtri = null)
        {
            return await context.Attività_SetAsync(filtri);
        }
        [HttpGet("AttivitaDel/{ID}")]
        public async Task<Allert> Attivita_DeleteAsync(int ID)
        {
            return await context.Attività_DeleteAsync(ID);
        }
        #endregion

        #region <<ATTIVITA CEDUTA>>
        [HttpPost("AttivitaCedutaSet")]
        public async Task<Allert> AttivitàCeduta_SetAsync([FromBody] AttivitaCedutaDTO filtri = null)
        {
            return await context.AttivitàCeduta_SetAsync(filtri);
        }
        #endregion


        #region <<COMPILAZIONE ORE>>


        [HttpGet("CompilazioneOreGetList")]
        public async Task<List<CompilazioneOreDTO>> CompilazioneOre_GetList([FromBody] CompilazioneOreFM filtri = null)
        {
            var obj = await context.CompilazioneOre_GetListAsync(filtri);
            return obj;
        }

        [HttpGet("CompilazioneOreGetByID/{ID}")]
        public async Task<CompilazioneOreDTO> CompilazioneOre_GetByIdAsync(int ID)
        {
            return await context.CompilazioneOre_GetByIdAsync(ID);
        }

        [HttpPost("CompilazioneOreSet")]
        public async Task<Allert> CompilazioneOre_SetAsync([FromBody] CompilazioneOreFM filtri = null)
        {
            return await context.CompilazioneOre_SetAsync(filtri);
        }
        [HttpDelete("CompilazioneOreDel/{ID}")]
        public async Task<Allert> CompilazioneOre_DeleteAsync(int ID)
        {
            return await context.CompilazioneOre_DeleteAsync(ID);
        }
        #endregion

        #region <<UTENTE>>

        [HttpGet("UtenteGetList")]
        public async Task<List<UtenteDTO>> Utente_GetListAsync([FromBody] UtenteFM utenteFM = null)
        {
            var obj = await context.Utente_GetListAsync(utenteFM);
            return obj;
        }
        [HttpGet("UtenteGetByID/{ID}")]
        public async Task<UtenteDTO> Utente_GetByIdAsync(int ID)
        {
            return await context.Utente_GetByIdAsync(ID);
        }

        [HttpGet("UtenteGetUserPassword/{Username}/{Password}")]
        public async Task<UtenteDTO> Utente_GetByUserAndPasswordAsync(string Username = "", string Password = "")
        {
            UtenteFM utenteFM = new UtenteFM() { Username = Username, Password = Password };
            return await context.Utente_GetByUserAndPasswordAsync(utenteFM);
        }

        [HttpPost("UtenteSet")]
        public async Task<Allert> Utente_SetAsync([FromBody] UtenteFM filtri = null)
        {
            return await context.Utente_SetAsync(filtri);
        }
        [HttpDelete("UtenteDel/{ID}")]
        public string Utente_DeleteAsync(int ID)
        {
            return context.Utente_DeleteAsync(ID);
        }
        #endregion

        //#region <<AZIENDA>>
        //[HttpGet("AziendaGetList/{ID}/{Azienda}")]
        //public async Task<List<AziendaDTO>> Azienda_GetListAsync(int ID = 0, string Azienda = "")
        //{
        //    AziendaFM aziendaFM = new AziendaFM() { ID = ID, Azienda = Azienda };
        //    return await context.Azienda_GetListAsync(aziendaFM);
        //}
        //[HttpGet("AziendaGetByID/{ID}")]
        //public async Task<AziendaDTO> Azienda_GetByIdAsync(int id)
        //{
        //    return await context.Azienda_GetByIdAsync(id);
        //}
        //[HttpPost("AziendaSet")]
        //public async Task<(string returnMessage, int? Result)> Azienda_SetAsync([FromBody] AziendaDTO filtri = null)
        //{
        //    return await context.Azienda_SetAsync(filtri);
        //}
        //[HttpDelete("AziendaDel/{ID}")]
        //public string Azienda_DeleteAsync(int ID)
        //{
        //    return context.Azienda_DeleteAsync(ID);
        //}
        //#endregion

        //#region <<AZIENDA UTENTE>>
        //[HttpGet("UtenteAziendaGetList/{Modalità}/{ID}/{IDAzienda}/{IDUtente}")]
        //public async Task<List<UtenteAziendaDTO>> UtenteAzienda_GetListAsync(int Modalità = 0, int ID = 0, int IDAzienda = 0, int IDUtente = 0)
        //{
        //    UtenteAziendaFM utenteAziendaFM = new UtenteAziendaFM() { Modalità = Modalità, ID = ID, IDAzienda = IDAzienda, IDUtente = IDUtente };
        //    return await context.UtenteAzienda_GetListAsync(utenteAziendaFM);
        //}
        //[HttpGet("UtenteAziendaGetByID/{ID}")]
        //public async Task<UtenteDTO> UtenteAzienda_GetByIdAsync(int ID)
        //{
        //    return await context.Utente_GetByIdAsync(ID);
        //}
        //[HttpPost("UtenteAziendaSet")]
        //public async Task<(string returnMessage, int? Result)> UtenteAzienda_SetAsync([FromBody] UtenteAziendaDTO parameter = null)
        //{
        //    return await context.UtenteAzienda_SetAsync(parameter);
        //}
        //[HttpDelete("UtenteAziendaDel/{ID}")]
        //public string UtenteAzienda_DeleteAsync(int IDUtenteAzienda)
        //{
        //    return context.UtenteAzienda_DeleteAsync(IDUtenteAzienda);
        //}
        //#endregion

        //#region <<NOTE>>
        //[HttpGet("NotaGetList/{ID}")]
        //public async Task<List<NotaDTO>> Nota_GetListAsync(int ID)
        //{
        //    NotaFM notaFM = new NotaFM() { ID = ID};
        //    return await context.Nota_GetListAsync(notaFM);
        //}
        //[HttpGet("NotaGetByID/{ID}")]
        //public async Task<NotaDTO> Nota_GetByIdAsync(int id)
        //{
        //    return await context.Nota_GetByIdAsync(id);
        //}
        //[HttpPost("NotaSet")]
        //public async Task<(string returnMessage, int? Result)> Nota_SetAsync([FromBody] NotaDTO parameter)
        //{
        //    return await context.Nota_SetAsync(parameter);
        //}
        //[HttpDelete("NotaDel/{ID}/{TipoID}")]
        //public string Nota_DeleteAsync(int ID, string? TipoID)
        //{
        //    return context.Nota_DeleteAsync(ID, TipoID);
        //}
        //#endregion
    }
}
