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
    public partial class ActividadesVista : ContentPage
    {
        private HttpClient cliente;
        Evento evento;

        public ActividadesVista(Evento evento)
        {
            InitializeComponent();
            this.evento = evento;
            cliente = new HttpClient();
            EntryDatePickerFecha.NullableDate = null;
        }

        protected override async void OnAppearing()
        {
            Uri uri = new Uri(Recurso.URI + "Eventos/" + evento.ID + "/Actividades");

            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = uri;
            request.Method = HttpMethod.Get;
            request.Headers.Add("Accept", "application/json");

            HttpResponseMessage respuesta = await cliente.SendAsync(request);

            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                string contenido = await respuesta.Content.ReadAsStringAsync();
                IEnumerable<Actividad> actividades = JsonConvert.DeserializeObject<IEnumerable<Actividad>>(contenido);
                ListaActividades.ItemsSource = actividades;
            }
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
        private async void AddActividadClicked(object sender, EventArgs e)
        {
            Actividad actividad = new Actividad
            {
                EventoID = evento.ID,
                Titulo = EntryTitulo.Text,
                Descripcion = EntryDescripcion.Text
            };

            if (EntryDatePickerFecha.NullableDate.Equals(null))
            {
                actividad.FechaHora = null;
            }
            else
            {
                actividad.FechaHora = EntryDatePickerFecha.Date;
            }

            Uri uri = new Uri(Recurso.URI + "Actividades");

            string json = JsonConvert.SerializeObject(actividad);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await cliente.PostAsync(uri, content);

            if (response.StatusCode == HttpStatusCode.Created)
            {
                await DisplayAlert("Genial", "Se ha añadido la actividad", "Ok");
                OnAppearing();
            }
            else
            {
                await DisplayAlert("Atención", "No se ha podido añadir la actividad", "Ok");
            }

            EntryTitulo.Text = "";
            EntryDescripcion.Text = "";
            EntryDatePickerFecha.NullableDate = null;
        }

        private async void EliminarClicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int ID = Convert.ToInt32(button.CommandParameter.ToString());

            Uri uri = new Uri(Recurso.URI + "Actividades/" + ID);

            HttpResponseMessage response = await cliente.DeleteAsync(uri);

            if (response.StatusCode != HttpStatusCode.NoContent)
            {
                await DisplayAlert("Atención", "Ha surgido un problema", "Ok");
            }

            OnAppearing();
        }

    }
}