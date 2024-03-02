using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taurinorum.Object;
using Taurinorum.Object.DataTrasportObject;
using Taurinorum.Timbratrice.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Taurinorum.Timbratrice.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TimbraPage : ContentPage
    {
        private TimbraViewModel viewModel;
        public TimbraPage(int IdDipendente)
        {
            this.BindingContext = viewModel = new TimbraViewModel(IdDipendente);

            NavigationPage.SetHasNavigationBar(this, false);
            NavigationPage.SetHasBackButton(this, false);

            InitializeComponent();
        }

        private async void Timbra_Clicked(object sender, EventArgs e)
        {
            Allert obj = await viewModel.Timbra();

            await DisplayAlert("Messaggio", obj.Messaggio, "conferma");
            ModificaBottone();
            OnAppearing();
        }

        protected override async void OnAppearing()
        {
            ModificaBottone();
        }

        private async void ModificaBottone()
        {
            viewModel.UltimaTimbratura = await viewModel.CercaUltimTimbratura();
            if(viewModel.UltimaTimbratura == null)
            {
                ComeTimbrare.Text = "SCHIACCIA SU TIMBRA PER ENTRARE A LAVORO";
                ButtonTimbrare.BackgroundColor = Color.Green;
            }
            else
            {
                if (viewModel.UltimaTimbratura.EntUsc)
                {
                    ComeTimbrare.Text = "SCHIACCIA SU TIMBRA PER USCIRE DA LAVORO";
                    ButtonTimbrare.BackgroundColor = Color.Red;
                }
                else
                {
                    ComeTimbrare.Text = "SCHIACCIA SU TIMBRA PER ENTRARE A LAVORO";
                    ButtonTimbrare.BackgroundColor = Color.Green;
                }
            }
        }

    }
}