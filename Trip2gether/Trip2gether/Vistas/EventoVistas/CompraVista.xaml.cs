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
    public partial class CompraVista : ContentPage
    {
        private HttpClient cliente;
        Usuario usuario;
        Evento evento;

        public CompraVista(Usuario usuario, Evento evento)
        {
            InitializeComponent();
            this.usuario = usuario;
            this.evento = evento;
            cliente = new HttpClient();
        }

        protected override async void OnAppearing()
        {
            Uri uri = new Uri(Recurso.URI + "Eventos/" + evento.ID + "/Articulos");

            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = uri;
            request.Method = HttpMethod.Get;
            request.Headers.Add("Accept", "application/json");

            HttpResponseMessage respuesta = await cliente.SendAsync(request);

            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                string contenido = await respuesta.Content.ReadAsStringAsync();
                IEnumerable<Articulo> compra = JsonConvert.DeserializeObject<IEnumerable<Articulo>>(contenido);
                ListaCompra.ItemsSource = compra;
            }

            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        private async void AddArticuloClicked(object sender, EventArgs e)
        {
            Articulo articulo = new Articulo
            {
                EventoID = evento.ID,
                Nombre = EntryNombre.Text
            };

            Uri uri = new Uri(Recurso.URI + "Articulos");

            string json = JsonConvert.SerializeObject(articulo);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await cliente.PostAsync(uri, content);

            if (response.StatusCode == HttpStatusCode.Created)
            {
                await DisplayAlert("Genial", "Se ha añadido el artículo", "Ok");
                OnAppearing();
            }
            else
            {
                await DisplayAlert("Atención", "No se ha podido añadir el artículo", "Ok");
            }

            EntryNombre.Text = "";
        }

        private async void SeleccionadoChanged(object sender, EventArgs e)
        {
            var checkbox = (CheckBox)sender;
            var articulo = checkbox.BindingContext as Articulo;

            if (articulo != null)
            {
                articulo.Seleccionado = checkbox.IsChecked;                

                Uri uri = new Uri(Recurso.URI + "Articulos/" + articulo.ID);

                string json = JsonConvert.SerializeObject(articulo);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await cliente.PutAsync(uri, content);

                if (response.StatusCode != HttpStatusCode.NoContent)
                {
                    await DisplayAlert("Atención", "Ha ocurrido un error", "Ok");
                }
            }
        }

        private async void EliminarClicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int ID = Convert.ToInt32(button.CommandParameter.ToString());

            Uri uri = new Uri(Recurso.URI + "Articulos/" + ID);

            HttpResponseMessage response = await cliente.DeleteAsync(uri);

            if (response.StatusCode != HttpStatusCode.NoContent)
            {
                await DisplayAlert("Atención", "Ha surgido un problema", "Ok");
            }

            OnAppearing();
        }


    }
}