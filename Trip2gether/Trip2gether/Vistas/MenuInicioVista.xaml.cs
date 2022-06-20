using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trip2gether_API.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Trip2gether.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuInicioVista : TabbedPage
    {
        Usuario usuario;
        public MenuInicioVista(Usuario usuario)
        {
            InitializeComponent();
            this.usuario = usuario;

            this.Title = "Inicio";
            this.Children.Add(new ListaEventoVista(usuario) { Title = "Eventos" });
            this.Children.Add(new PerfilVista(usuario) { Title = "Perfil" });

        }

    }

}