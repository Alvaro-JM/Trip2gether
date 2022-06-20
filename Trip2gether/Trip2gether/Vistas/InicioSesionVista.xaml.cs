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
    public partial class InicioSesionVista : ContentPage
    {
        private HttpClient cliente;

        public InicioSesionVista()
        {
            InitializeComponent();
            cliente = new HttpClient();
            Logo.Source = ImageSource.FromFile("Trip2gether_logo.png");
            Logo.Aspect = Aspect.AspectFit;
        }

        private async void InicioSesionClicked(object sender, EventArgs e)
        {
            Uri uri = new Uri(Recurso.URI + "Usuarios/user=" + EntryNombre.Text + "/pass=" + EntryPassword.Text);

            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = uri;
            request.Method = HttpMethod.Get;
            request.Headers.Add("Accept", "application/json");

            HttpResponseMessage respuesta = await cliente.SendAsync(request);

            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                string content = await respuesta.Content.ReadAsStringAsync();
                Usuario usuario = JsonConvert.DeserializeObject<Usuario>(content);
                await Navigation.PushModalAsync(new MenuInicioVista(usuario));
            }
            else
            {
                await DisplayAlert("Atención", "Nombre o password erróneo", "Ok");
                EntryPassword.Text = "";
            }
        }

        private void NuevoUsuarioClicked(object sender, EventArgs e)
        {
            this.Navigation.PushModalAsync(new NuevoUsuarioVista());
        }

    }
}