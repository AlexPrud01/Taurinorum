using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Taurinorum.Object.DataTrasportObject;
using Taurinorum.Object.FilterModel;
using Taurinorum.Object;
using Taurinorum.Timbratrice.Views;
using Xamarin.Forms;
using Xamarin.Essentials;
using Taurinorum.Timbratrice.Services;

namespace Taurinorum.Timbratrice.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        HttpClient client;
        public Command LoginCommand { get; }
        public UtenteDTO UtenteLog { get; set; }
        public UtenteFM utenteFM { get; set; }
        RestService restService { get; set; }
        public int IdDipendente;

        public LoginViewModel()
        {
            utenteFM = new UtenteFM();
            restService = new RestService();
        }

        public async Task<bool> VerificaLog()
        {

            var obj = SecureStorage.GetAsync("IDDipendete");
            try
            {
                IdDipendente = Convert.ToInt32(obj.Result);

                if (IdDipendente != null && IdDipendente > 0)
                    return true;
                else
                    return false;

            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> Login()
        {
            try
            {
                UtenteLog = await restService.Utente_GetByUserAndPasswordAsync(utenteFM);

                if (UtenteLog != null)
                {

                    SecureStorage.SetAsync("IDDipendete", UtenteLog.ID.ToString());

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                UtenteLog = new UtenteDTO();
                return false;
            }
        }
    }
}
