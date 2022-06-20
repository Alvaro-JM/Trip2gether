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
    public partial class ListaEventoVista : ContentPage
    {
        private HttpClient cliente;
        Usuario usuario;

        public ListaEventoVista(Usuario usuario)
        {
            InitializeComponent();
            this.usuario = usuario;
            cliente = new HttpClient();
        }

        protected override async void OnAppearing()
        {
            Uri uri = new Uri(Recurso.URI + "Usuarios/" + usuario.ID + "/Eventos");

            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = uri;
            request.Method = HttpMethod.Get;
            request.Headers.Add("Accept", "application/json");

            HttpResponseMessage respuesta = await cliente.SendAsync(request);

            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                string contenido = await respuesta.Content.ReadAsStringAsync();
                IEnumerable<Evento> eventos = JsonConvert.DeserializeObject<IEnumerable<Evento>>(contenido);
                ListaEventos.ItemsSource = eventos;

                ListaEventos.ItemSelected += (sender, e) =>
                {
                    Evento eventoSeleccionado = (Evento)e.SelectedItem;
                    
                    if (eventoSeleccionado != null)
                    {
                        this.Navigation.PushModalAsync(new EventoVista(usuario, eventoSeleccionado));
                    }
                };
            }
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        private async void AddEventoClicked(object sender, EventArgs e)
        {
            if (EntryNombre.Text != "" && EntryPassword.Text != ""
                && EntryNombre.Text != null && EntryPassword.Text != null)
            {
                Uri uri = new Uri(Recurso.URI + "Eventos/name=" + EntryNombre.Text + " /pass=" + EntryPassword.Text);

                HttpRequestMessage request = new HttpRequestMessage();
                request.RequestUri = uri;
                request.Method = HttpMethod.Get;
                request.Headers.Add("Accept", "application/json");

                HttpResponseMessage respuesta = await cliente.SendAsync(request);

                if (respuesta.StatusCode == HttpStatusCode.OK)
                {
                    string contenido = await respuesta.Content.ReadAsStringAsync();
                    Evento evento = JsonConvert.DeserializeObject<Evento>(contenido);

                    Participacion participacion = new Participacion
                    {
                        UsuarioID = usuario.ID,
                        EventoID = evento.ID
                    };

                    Uri uri2 = new Uri(Recurso.URI + "Participaciones");

                    string json = JsonConvert.SerializeObject(participacion);
                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await cliente.PostAsync(uri2, content);

                    if (response.StatusCode == HttpStatusCode.Created)
                    {
                        await DisplayAlert("Genial", "Se ha añadido el evento", "Ok");
                        OnAppearing();
                    }
                    else
                    {
                        await DisplayAlert("Atención", "No se ha podido añadir el evento", "Ok");
                    }
                }
                else
                {
                    await DisplayAlert("Atención", "Nombre y/o clave erróneos", "Ok");
                }

                EntryNombre.Text = "";
                EntryPassword.Text = "";
            }
            else
            {
                await DisplayAlert("Atención", "Debes rellenar ambos campos", "Ok");
            }            
        }

        private void NuevoEventoClicked(object sender, EventArgs e)
        {
            this.Navigation.PushModalAsync(new NuevoEventoVista(usuario));
        }
    }
}