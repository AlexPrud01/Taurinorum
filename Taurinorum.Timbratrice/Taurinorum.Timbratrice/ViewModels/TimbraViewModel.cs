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
using static Xamarin.Forms.Internals.Profile;
using Taurinorum.Timbratrice.Services;
using System.Timers;
using Taurinorum.Object.FilterModel.EmissioneFoglio;

namespace Taurinorum.Timbratrice.ViewModels
{
    public class TimbraViewModel : BaseViewModel
    {
        public int IdUtente { get; set; }
        public UtenteFM utenteFM { get; set; }
        public TimbraFM timbraFM { get; set; }
        RestService restService { get; set; }

        private readonly Timer _timer;

        private string _currentTime;

        public TimbraDTO UltimaTimbratura { get; set; }

        public string CurrentTime
        {
            get { return _currentTime; }
            set
            {
                _currentTime = value;
                OnPropertyChanged(nameof(CurrentTime));
            }
        }

        public TimbraViewModel(int IdUtente)
        {
            restService = new RestService();

            this.IdUtente = IdUtente;
            // Inizializza e avvia un timer per aggiornare l'ora ogni secondo
            timbraFM = new TimbraFM() { IDUtente = IdUtente }; 
            CurrentTime = DateTime.Now.ToString("HH:mm:ss");
            _timer = new Timer(1000);
            _timer.Elapsed += OnTimerElapsed;
            _timer.Start();

        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            // Aggiorna l'ora corrente ogni secondo
            Device.BeginInvokeOnMainThread(() =>
            {
                CurrentTime = DateTime.Now.ToString("HH:mm:ss");
            });
        }

        public async Task<TimbraDTO> CercaUltimTimbratura()
        {
            return await restService.Timbra_GetByIdUtenteAsync(IdUtente);
        }

        public async Task<Allert> Timbra()
        {
            UltimaTimbratura = await CercaUltimTimbratura();

            timbraFM.EntUsc = true;

            if (UltimaTimbratura != null)
            {
                TimeSpan Differenza = DateTime.Now - UltimaTimbratura.DataOra;
                if (Differenza.TotalMinutes < 10)
                {
                    return new Allert() { Messaggio = "Hai già timbrato " + (int)Differenza.TotalMinutes + " minuti fa", Valore = 0 };
                }
                timbraFM.EntUsc = !UltimaTimbratura.EntUsc;
            }
            await TrovaPosizione();
            return await restService.Timbra_SetAsync(timbraFM);
        }


        private async Task TrovaPosizione()
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    timbraFM.Latitudine = (decimal)location.Latitude;
                    timbraFM.Longitudine = (decimal)location.Longitude;

                    // Fai qualcosa con le coordinate (latitude, longitude) qui
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Gestisci l'eccezione per dispositivi non supportati
            }
            catch (PermissionException pEx)
            {
                // Gestisci l'eccezione per i permessi negati
            }
            catch (Exception ex)
            {
                // Gestisci altre eccezioni
            }
        }
    }
}
