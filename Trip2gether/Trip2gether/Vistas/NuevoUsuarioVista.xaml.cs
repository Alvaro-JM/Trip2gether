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
    public partial class NuevoUsuarioVista : ContentPage
    {
        private HttpClient cliente;

        public NuevoUsuarioVista()
        {
            InitializeComponent();
            cliente = new HttpClient();
        }

        private async void CrearUsuarioClicked(object sender, EventArgs e)
        {
            if (EntryNombre.Text != "" && EntryNombre.Text != null
                && EntryEmail.Text != "" && EntryEmail.Text != null
                && EntryPassword.Text != "" && EntryPassword.Text != null
                && EntryPassword2.Text != "" && EntryPassword2.Text != null)
            {
                if (EntryPassword.Text == EntryPassword2.Text)
                {
                    Usuario usuario = new Usuario
                    {
                        Nombre = EntryNombre.Text,
                        Mail = EntryEmail.Text,
                        Password = EntryPassword.Text
                    };

                    Uri uri = new Uri(Recurso.URI + "Usuarios");

                    string json = JsonConvert.SerializeObject(usuario);
                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await cliente.PostAsync(uri, content);

                    if (response.StatusCode == HttpStatusCode.Created)
                    {                        
                        await DisplayAlert("Genial", "Se ha creado el usuario", "Ok");

                        EntryNombre.Text = "";
                        EntryEmail.Text = "";
                        EntryPassword.Text = "";
                        EntryPassword2.Text = "";

                        await this.Navigation.PopModalAsync(true);
                    }
                    else
                    {
                        await DisplayAlert("Atención", "No se ha podido añadir el usuario.\nEl nombre o el email ya está en uso", "Ok");
                    }
                }
                else
                {
                    await DisplayAlert("Atención", "Los campos con el password no coinciden", "Ok");
                }
            }
            else
            {
                await DisplayAlert("Atención", "Debes rellenar todos los campos", "Ok");
            }            
        }

    }    
}