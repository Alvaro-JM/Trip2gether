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

namespace Trip2gether.Vistas.EventoVistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ValoracionesVista : ContentPage
    {
        private HttpClient cliente;
        Usuario usuario;
        Evento evento;

        public ValoracionesVista(Usuario usuario, Evento evento)
        {
            InitializeComponent();
            this.usuario = usuario;
            this.evento = evento;
            cliente = new HttpClient();
        }

        protected override async void OnAppearing()
        {
            Uri uri = new Uri(Recurso.URI + "Eventos/" + evento.ID + "/Valoraciones");

            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = uri;
            request.Method = HttpMethod.Get;
            request.Headers.Add("Accept", "application/json");

            HttpResponseMessage  respuesta = await cliente.SendAsync(request);

            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                string contenido = await respuesta.Content.ReadAsStringAsync();
                IEnumerable<Valoracion> valoraciones = JsonConvert.DeserializeObject<IEnumerable<Valoracion>>(contenido);
                ListaValoraciones.ItemsSource = valoraciones;
            }
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        private async void AddValoracionClicked(object sender, EventArgs e)
        {
            if (EntryNumero.Text == null || EntryObservacion.Text == null)
            {
                await DisplayAlert("Atención", "Debes rellenar ambos campos", "Ok");
            }
            else
            {
                try
                {
                    int num = Convert.ToInt32(EntryNumero.Text);

                    if (num < 1 || num > 10)
                    {
                        throw new ArgumentOutOfRangeException();
                    }

                    Valoracion valoracion = new Valoracion
                    {
                        EventoID = evento.ID,
                        UsuarioID = usuario.ID,
                        Numero = num,
                        Observacion = EntryObservacion.Text
                    };

                    Uri uri = new Uri(Recurso.URI + "Valoraciones");

                    string json = JsonConvert.SerializeObject(valoracion);
                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await cliente.PostAsync(uri, content);

                    if (response.StatusCode == HttpStatusCode.Created)
                    {
                        await DisplayAlert("Genial", "Se ha añadido tu valoración", "Ok");
                        OnAppearing();
                    }
                    else
                    {
                        await DisplayAlert("Atención", "Solo puedes valorar el evento una vez", "Ok");
                    }

                    EntryNumero.Text = "";
                    EntryObservacion.Text = "";
                }
                catch (Exception)
                {
                    await DisplayAlert("Atención", "La valoración debe ser un número entero entre 1 y 10", "Ok");
                    EntryNumero.Text = "";
                }
            }
        }

        private async void EliminarClicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            var valoracion = button.BindingContext as Valoracion;

            if (valoracion.UsuarioID == usuario.ID)
            {
                Uri uri = new Uri(Recurso.URI + "Valoraciones/" + valoracion.ID);

                HttpResponseMessage response = await cliente.DeleteAsync(uri);

                if (response.StatusCode != HttpStatusCode.NoContent)
                {
                    await DisplayAlert("Atención", "Ha surgido un problema", "Ok");
                }

                OnAppearing();
            }
            else
            {
                await DisplayAlert("Atención", "No puedes eliminar la valoración de otro usuario", "Ok");
            }

        }
    }
}