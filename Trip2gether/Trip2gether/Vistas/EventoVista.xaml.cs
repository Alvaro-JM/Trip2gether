using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trip2gether.Vistas.EventoVistas;
using Trip2gether_API.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Trip2gether.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventoVista : FlyoutPage
    {
        public Usuario usuario;
        public Evento evento;

        public EventoVista(Usuario usuario, Evento evento )
        {
            InitializeComponent();
            FlyoutPage.ListView.ItemSelected += ListView_ItemSelected;
            this.usuario = usuario;
            this.evento = evento;
            Detail = new NavigationPage(new EventoDatosVista(usuario, evento));
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            int item = e.SelectedItemIndex;
            Page page;

            switch (item)
            {
                case 1:
                    page = new TareasVista(usuario, evento);
                    break;
                case 2:
                    page = new CompraVista(usuario, evento);
                    break;
                case 3:
                    page = new ActividadesVista(evento);
                    break;
                case 4:
                    page = new GastosVista(usuario, evento);
                    break;
                case 5:
                    page = new RepartoGastosVista(evento);
                    break;
                case 6:
                    page = new ValoracionesVista(usuario, evento);
                    break;
                default:
                    page = new EventoDatosVista(usuario, evento);
                    break;
            }
            page.Title = evento.Nombre;
            Detail = new NavigationPage(page);

            IsPresented = false;

            FlyoutPage.ListView.SelectedItem = null;
        }

    }
}