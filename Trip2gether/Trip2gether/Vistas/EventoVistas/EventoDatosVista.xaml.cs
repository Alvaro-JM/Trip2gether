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
    public partial class EventoDatosVista : ContentPage
    {
        private HttpClient cliente;
        Usuario usuario;
        Evento evento;
        Image Imagen = new Image();
        public string Titulo { get; set; }

        public EventoDatosVista(Usuario usuario, Evento evento)
        {
            InitializeComponent();
            this.usuario = usuario;
            this.evento = evento;
            cliente = new HttpClient();

            BindingContext = this;
            Titulo = evento.Nombre;

            Imagen.Source = ImageSource.FromFile("user_icon.png");
            Imagen.Aspect = Aspect.AspectFit;
        }

        protected override async void OnAppearing()
        {
            CancelarButton.IsVisible = false;
            GuardarButton.IsVisible = false;
            ModificarButton.IsVisible = true;

            EntryFechaInicio.NullableDate = evento.FechaInicio;
            EntryFechaInicio.IsEnabled = false;

            EntryFechaFin.NullableDate = evento.FechaFin;
            EntryFechaFin.IsEnabled = false;

            EntryDestino.Text = evento.Destino;
            EntryDestino.IsReadOnly = true;

            EntryAlojamiento.Text = evento.Alojamiento;
            EntryAlojamiento.IsReadOnly = true;

            EntryClave.Text = evento.Password;
            EntryClave.IsReadOnly = true;

            Uri uri = new Uri(Recurso.URI + "Eventos/" + evento.ID + "/Usuarios");

            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = uri;
            request.Method = HttpMethod.Get;
            request.Headers.Add("Accept", "application/json");

            HttpResponseMessage respuesta = await cliente.SendAsync(request);

            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                string contenido = await respuesta.Content.ReadAsStringAsync();
                IEnumerable<Usuario> usuarios = JsonConvert.DeserializeObject<IEnumerable<Usuario>>(contenido);
                ListaUsuarios.ItemsSource = usuarios;
            }
            base.OnAppearing();
        }

        private void ModificarClicked(object sender, EventArgs e)
        {
            CancelarButton.IsVisible = true;
            GuardarButton.IsVisible = true;
            ModificarButton.IsVisible = false;

            EntryFechaInicio.IsEnabled = true;
            EntryFechaFin.IsEnabled = true;
            EntryDestino.IsReadOnly = false;
            EntryAlojamiento.IsReadOnly = false;
            EntryClave.IsReadOnly = false;
        }

        private void CancelarClicked(object sender, EventArgs e)
        {
            OnAppearing();
        }

        private async void GuardarClicked(object sender, EventArgs e)
        {
            if (EntryClave.Text != "")
            {

                evento.FechaInicio = EntryFechaInicio.NullableDate;
                evento.FechaFin = EntryFechaFin.NullableDate;
                evento.Destino = EntryDestino.Text;
                evento.Alojamiento = EntryAlojamiento.Text;
                evento.Password = EntryClave.Text;

                Uri uri = new Uri(Recurso.URI + "Eventos/" + evento.ID);

                string json = JsonConvert.SerializeObject(evento);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await cliente.PutAsync(uri, content);

                if (response.StatusCode != HttpStatusCode.NoContent)
                {
                    await DisplayAlert("Atención", "Ha ocurrido un error", "Ok");
                }
            }
            else
            {
                await DisplayAlert("Atención", "El evento debe tener una clave de acceso", "Ok");
            }

            OnAppearing();
        }

    }
}