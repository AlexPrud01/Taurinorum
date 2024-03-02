using System;
using Taurinorum.Timbratrice.Services;
using Taurinorum.Timbratrice.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Taurinorum.Timbratrice
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
