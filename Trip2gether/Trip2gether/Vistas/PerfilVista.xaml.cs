using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Trip2gether.Recursos;
using Trip2gether_API.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Trip2gether.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PerfilVista : ContentPage
    {
        Usuario usuario;
        private HttpClient cliente;

        public PerfilVista(Usuario usuario)
        {
            InitializeComponent();
            this.usuario = usuario;
            cliente = new HttpClient();
            ImagenPerfil.Source = ImageSource.FromFile("user_icon.png");
            ImagenPerfil.Aspect = Aspect.AspectFit;
        }

        protected override void OnAppearing()
        {
            UserName.Text = usuario.Nombre;
            UserName.IsReadOnly = true;

            UserMail.Text = usuario.Mail;
            UserMail.IsReadOnly = true;

            UserPassword.Text = usuario.Password;
            UserPassword.IsReadOnly = true;

            CancelarButton.IsVisible = false;
            GuardarButton.IsVisible = false;
            ModificarButton.IsVisible = true;

            base.OnAppearing();
        }

        private void ModificarClicked(object sender, EventArgs e)
        {
            CancelarButton.IsVisible = true;
            GuardarButton.IsVisible = true;
            ModificarButton.IsVisible = false;

            UserName.IsReadOnly = false;
            UserMail.IsReadOnly = false;
            UserPassword.IsReadOnly = false;
        }

        private void CancelarClicked(object sender, EventArgs e)
        {
            OnAppearing();
        }

        private async void GuardarClicked(object sender, EventArgs e)
        {
            if (UserName.Text != "" 
                && UserMail.Text != ""
                && UserPassword.Text != "")
            {
                usuario.Nombre = UserName.Text;
                usuario.Mail = UserMail.Text;
                usuario.Password = UserPassword.Text;

                Uri uri = new Uri(Recurso.URI + "Usuarios/" + usuario.ID);

                string json = JsonConvert.SerializeObject(usuario);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await cliente.PutAsync(uri, content);

                if (response.StatusCode != HttpStatusCode.NoContent)
                {
                    await DisplayAlert("Atención", "El nombre de usuario o el email ya está en uso", "Ok");
                }
            }
            else
            {
                await DisplayAlert("Atención", "Debes rellenar todos los campos", "Ok");
            }

            OnAppearing();
        }
    }
}