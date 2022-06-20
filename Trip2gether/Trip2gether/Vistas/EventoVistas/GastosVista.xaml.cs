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
    public partial class GastosVista : ContentPage
    {
        private HttpClient cliente;
        Usuario usuario;
        Evento evento;

        public GastosVista(Usuario usuario, Evento evento)
        {
            InitializeComponent();
            this.usuario = usuario;
            this.evento = evento;
            cliente = new HttpClient();
        }

        protected override async void OnAppearing()
        {
            Uri uri = new Uri(Recurso.URI + "Eventos/" + evento.ID + "/Gastos");

            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = uri;
            request.Method = HttpMethod.Get;
            request.Headers.Add("Accept", "application/json");

            HttpResponseMessage respuesta = await cliente.SendAsync(request);

            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                string contenido = await respuesta.Content.ReadAsStringAsync();
                IEnumerable<Gasto> gastos = JsonConvert.DeserializeObject<IEnumerable<Gasto>>(contenido);
                ListaGastos.ItemsSource = gastos;
            }
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        private async void AddGastoClicked(object sender, EventArgs e)
        {
            if (EntryCuantia.Text != "" && EntryCuantia.Text != null
                && EntryConcepto.Text != "" && EntryConcepto.Text != null)
            {
                Gasto gasto = new Gasto
                {
                    EventoID = evento.ID,
                    UsuarioID = usuario.ID,
                    Cuantia = Convert.ToDouble(EntryCuantia.Text),
                    Concepto = EntryConcepto.Text,
                };

                Uri uri = new Uri(Recurso.URI + "Gastos");

                string json = JsonConvert.SerializeObject(gasto);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await cliente.PostAsync(uri, content);

                if (response.StatusCode == HttpStatusCode.Created)
                {
                    await DisplayAlert("Genial", "Se ha añadido el gasto", "Ok");
                    OnAppearing();
                }
                else
                {
                    await DisplayAlert("Atención", "No se ha podido añadir el gasto", "Ok");
                }

                EntryCuantia.Text = "";
                EntryConcepto.Text = "";
            }
            else
            {
                await DisplayAlert("Atención", "Debes rellenar ambos campos", "Ok");
            }
            
        }

        private async void EliminarClicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            var gasto = button.BindingContext as Gasto;

            if (gasto.UsuarioID == usuario.ID)
            {
                Uri uri = new Uri(Recurso.URI + "Gastos/" + gasto.ID);

                HttpResponseMessage response = await cliente.DeleteAsync(uri);

                if (response.StatusCode != HttpStatusCode.NoContent)
                {
                    await DisplayAlert("Atención", "Ha surgido un problema", "Ok");
                }

                OnAppearing();
            }
            else
            {
                await DisplayAlert("Atención", "No puedes eliminar el gasto de otro usuario", "Ok");
            }

        }
    }
}