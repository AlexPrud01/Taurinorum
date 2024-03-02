using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taurinorum.Timbratrice.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace Taurinorum.Timbratrice.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        readonly LoginViewModel viewModel;

        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = viewModel = new LoginViewModel();
        }

        private async void Login_Clicked(object sender, EventArgs e)
        {
            try
            {
                bool loggato = await viewModel.Login();

                if (loggato)
                    await Navigation.PushAsync(new TimbraPage(viewModel.IdDipendente));
                else
                    await DisplayAlert("Errore", "Login fallito", "Riprova");
            }
            catch (Exception ex)
            {

            }
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if(await viewModel.VerificaLog())
                await Navigation.PushAsync(new TimbraPage(viewModel.IdDipendente));
        }
    }
}