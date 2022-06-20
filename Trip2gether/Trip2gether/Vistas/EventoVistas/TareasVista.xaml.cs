
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Trip2gether.Models;
using Trip2gether.Recursos;
using Trip2gether_API.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Trip2gether.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TareasVista : ContentPage
    {
        private Uri uri;
        private HttpClient cliente;
        private HttpResponseMessage respuesta;
        Usuario usuario;
        Evento evento;

        public TareasVista(Usuario usuario, Evento evento)
        {
            InitializeComponent();
            this.usuario = usuario;
            this.evento = evento;
            cliente = new HttpClient();
        }

        protected override async void OnAppearing()
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            uri = new Uri(Recurso.URI + "Eventos/" + evento.ID + "/Tareas");

            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = uri;
            request.Method = HttpMethod.Get;
            request.Headers.Add("Accept", "application/json");

            respuesta = await cliente.SendAsync(request);

            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                string contenido = await respuesta.Content.ReadAsStringAsync();
                IEnumerable<Tarea> tareas = JsonConvert.DeserializeObject<IEnumerable<Tarea>>(contenido, settings);
                ListaTareas.ItemsSource = tareas;

                //ListaUsuarios.ItemSelected += (sender, e) =>
                //{
                //    Usuario usuarioSeleccionado = (Usuario)e.SelectedItem;

                //    if (usuarioSeleccionado != null)
                //    {
                //        this.Navigation.PushModalAsync(new EventoVista(usuario, eventoSeleccionado));
                //    }
                //};
            }

            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        private async void AddTareaClicked(object sender, EventArgs e)
        {
            TareaPost tarea = new TareaPost
            {
                EventoID = evento.ID,
                Titulo = EntryTitulo.Text,
                Descripcion = EntryDescripcion.Text,
            };

            uri = new Uri(Recurso.URI + "Tareas");

            string json = JsonConvert.SerializeObject(tarea);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await cliente.PostAsync(uri, content);

            if (response.StatusCode == HttpStatusCode.Created)
            {
                await DisplayAlert("Genial", "Se ha añadido la tarea", "Ok");
                OnAppearing();
            }
            else
            {
                await DisplayAlert("Atención", "No se ha podido añadir la tarea", "Ok");
            }

            EntryTitulo.Text = "";
            EntryDescripcion.Text = "";
        }

        private async void SeleccionadoChanged(object sender, EventArgs e)
        {
            var checkbox = (CheckBox)sender;
            var tarea = checkbox.BindingContext as Tarea;

            if (tarea != null)
            {
                tarea.Seleccionado = checkbox.IsChecked;
                
                string json = "";

                if (tarea.Usuario == null)
                {
                    TareaPut tareaPut = new TareaPut
                    {
                        ID = tarea.ID,
                        EventoID = tarea.EventoID,
                        Titulo = tarea.Titulo,
                        Descripcion = tarea.Descripcion,
                        Seleccionado = tarea.Seleccionado
                    };

                    json = JsonConvert.SerializeObject(tareaPut);
                }
                else
                {
                    json = JsonConvert.SerializeObject(tarea);
                }

                Uri uri = new Uri(Recurso.URI + "Tareas/" + tarea.ID);

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

            Uri uri = new Uri(Recurso.URI + "Tareas/" + ID);

            HttpResponseMessage response = await cliente.DeleteAsync(uri);

            if (response.StatusCode != HttpStatusCode.NoContent)
            {
                await DisplayAlert("Atención", "Ha surgido un problema", "Ok");
            }

            OnAppearing();
        }
    }
}