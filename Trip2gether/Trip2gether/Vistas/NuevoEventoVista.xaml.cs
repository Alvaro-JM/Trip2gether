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
    public partial class NuevoEventoVista : ContentPage
    {
        Usuario usuario;
        private HttpClient cliente;

        public NuevoEventoVista(Usuario usuario)
        {
            InitializeComponent();
            this.usuario = usuario;
            cliente = new HttpClient();
            EntryDatePickerInicio.NullableDate = null;
            EntryDatePickerFin.NullableDate = null;
        }

        private async void CrearEventoClicked(object sender, EventArgs e)
        {
            if (EntryNombre.Text != "" && EntryNombre.Text != null
                && EntryPassword.Text != "" && EntryPassword.Text != null)
            {
                Evento evento = new Evento
                {
                    Nombre = EntryNombre.Text,
                    Password = EntryPassword.Text,
                    Destino = EntryDestino.Text,
                    Alojamiento = EntryAlojamiento.Text,
                    FechaInicio = EntryDatePickerInicio.NullableDate,
                    FechaFin = EntryDatePickerFin.NullableDate
                };

                Uri uri = new Uri(Recurso.URI + "Eventos");

                string json = JsonConvert.SerializeObject(evento);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await cliente.PostAsync(uri, content);

                //Una vez creado el evento se crea la participación del usuario con el evento
                if (response.StatusCode == HttpStatusCode.Created)
                {
                    string contenido = await response.Content.ReadAsStringAsync();

                    Evento eventoRespuesta = JsonConvert.DeserializeObject<Evento>(contenido);
                    Participacion participacion = new Participacion
                    {
                        UsuarioID = usuario.ID,
                        EventoID = eventoRespuesta.ID
                    };

                    Uri uri2 = new Uri(Recurso.URI + "Participaciones");
                    string json2 = JsonConvert.SerializeObject(participacion);
                    StringContent content2 = new StringContent(json2, Encoding.UTF8, "application/json");

                    HttpResponseMessage response2 = await cliente.PostAsync(uri2, content2);

                    if (response2.StatusCode == HttpStatusCode.Created)
                    {
                        await DisplayAlert("Genial", "Se ha creado el evento", "Ok");
                        OnAppearing();
                        EntryNombre.Text = "";
                        EntryPassword.Text = "";
                        await this.Navigation.PopModalAsync(true);
                    }
                }
                else
                {
                    await DisplayAlert("Atención", "No se ha podido añadir el evento", "Ok");
                }
            }
            else
            {
                await DisplayAlert("Atención", "Los campos con * son obligatorios", "Ok");
            }
                                 
        }

    }
}