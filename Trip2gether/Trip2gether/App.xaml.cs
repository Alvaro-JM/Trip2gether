using System;
using Trip2gether.Vistas;
using Trip2gether_API.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Trip2gether
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new InicioSesionVista();
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
