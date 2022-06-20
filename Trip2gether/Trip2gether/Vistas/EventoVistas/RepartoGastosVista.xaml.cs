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

namespace Trip2gether.Vistas.EventoVistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RepartoGastosVista : ContentPage
    {
        private HttpClient cliente;
        private double deudaPorUsuario;
        private List<Gasto> gastosTodos;
        Evento evento;

        public RepartoGastosVista(Evento evento)
        {
            InitializeComponent();
            this.evento = evento;
            cliente = new HttpClient();
            gastosTodos = new List<Gasto>();
        }

        protected override async void OnAppearing()
        {
            //Se obtienen los usuarios del evento
            Uri uri = new Uri(Recurso.URI + "Eventos/" + evento.ID + "/Usuarios");

            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = uri;
            request.Method = HttpMethod.Get;
            request.Headers.Add("Accept", "application/json");

            HttpResponseMessage respuesta = await cliente.SendAsync(request);

            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                string contenido = await respuesta.Content.ReadAsStringAsync();
                IEnumerable<UsuarioPago> usuarios = JsonConvert.DeserializeObject<IEnumerable<UsuarioPago>>(contenido);

                await GetDeudaPorPersona(usuarios.Count());

                //A cada usuario del evento se le calculan los pagos realizados
                foreach (var user in usuarios)
                {
                    user.Deuda = deudaPorUsuario;

                    Uri uri2 = new Uri(Recurso.URI + "Gastos/evento=" + evento.ID + "/usuario=" + user.ID);

                    HttpRequestMessage request2 = new HttpRequestMessage();
                    request2.RequestUri = uri2;
                    request2.Method = HttpMethod.Get;
                    request2.Headers.Add("Accept", "application/json");

                    HttpResponseMessage respuesta2 = await cliente.SendAsync(request2);

                    if (respuesta2.StatusCode == HttpStatusCode.OK)
                    {
                        string contenido2 = await respuesta2.Content.ReadAsStringAsync();
                        IEnumerable<Gasto> gastos = JsonConvert.DeserializeObject<IEnumerable<Gasto>>(contenido2);

                        foreach (var gasto in gastos)
                        {
                            gastosTodos.Add(gasto); //Se acumulan todos los gastos en una lista
                            user.Paga(gasto.Cuantia);
                        }
                    }
                }

                //Se crea una lista de usuarios ordenada por los pagos realizados
                List<UsuarioPago> usuariosOrdenados = new List<UsuarioPago>();
                
                for (int i = 0; i < usuarios.Count(); i++)
                {
                    bool introducido = false;

                    int indice = 0;
                    while (!introducido)
                    {
                        if (indice == usuariosOrdenados.Count)
                        {
                            usuariosOrdenados.Add(usuarios.ElementAt(i));
                            introducido = true;
                        }

                        if (usuarios.ElementAt(i).Deuda > usuariosOrdenados.ElementAt(indice).Deuda)
                        {
                            usuariosOrdenados.Insert(indice, usuarios.ElementAt(i));
                            introducido = true;
                        }
                            
                        indice++;
                    }                                      
                }

                //Se crean los pagos que deben realizar los usuarios a otros usuarios
                List<Pago> pagos = new List<Pago>();

                int indicePersona = usuariosOrdenados.Count - 1;

                foreach (var dador in usuariosOrdenados)
                {
                    while (dador.Deuda > 0)
                    {
                        UsuarioPago recibidor = usuariosOrdenados[indicePersona];

                        if (recibidor.Deuda < 0)
                        {
                            if (dador.Deuda <= Math.Abs(recibidor.Deuda))
                            {
                                double deuda = dador.Deuda;
                                pagos.Add(new Pago(dador, recibidor, deuda));
                                dador.Paga(deuda);
                                recibidor.Recibe(deuda);
                            }
                            else
                            {
                                double deuda = Math.Abs(recibidor.Deuda);
                                pagos.Add(new Pago(dador, recibidor,deuda));
                                dador.Paga(Math.Abs(deuda));
                                recibidor.Recibe(Math.Abs(deuda));                                
                            }
                        }
                        else
                        {
                            indicePersona--;
                        }
                    }
                }

                ListaPagos.ItemsSource = pagos;
                
            }
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        protected async Task GetDeudaPorPersona(int numUsuarios)
        {
            double pagosTotal = 0;
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

                foreach (var gasto in gastos)
                {
                    pagosTotal += gasto.Cuantia;
                }
            }

            deudaPorUsuario = pagosTotal / numUsuarios;
        }

        private async void EliminarGastosClicked(object sender, EventArgs e)
        {
            var eliminar = await DisplayAlert("Atención", "Se borrarán todos los gastos.\n¿Desea continuar?", "Ok", "Cancel");

            if (eliminar)
            {
                foreach (var gasto in gastosTodos)
                {
                    Uri uri = new Uri(Recurso.URI + "Gastos/" + gasto.ID);

                    HttpResponseMessage response = await cliente.DeleteAsync(uri);

                    if (response.StatusCode != HttpStatusCode.NoContent)
                    {
                        await DisplayAlert("Atención", "Ha surgido un problema", "Ok");
                    }
                }
                OnAppearing();
            }      
        }

    }
}