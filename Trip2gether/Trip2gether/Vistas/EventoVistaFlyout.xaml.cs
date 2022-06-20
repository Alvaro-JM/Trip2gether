using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Trip2gether.Vistas.EventoVistas;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Trip2gether.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventoVistaFlyout : ContentPage
    {
        public ListView ListView;

        public EventoVistaFlyout()
        {
            InitializeComponent();

            BindingContext = new EventoVistaFlyoutViewModel();
            ListView = MenuItemsListView;
        }

        class EventoVistaFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<EventoVistaFlyoutMenuItem> MenuItems { get; set; }

            public EventoVistaFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<EventoVistaFlyoutMenuItem>(new[]
                {
                    new EventoVistaFlyoutMenuItem { Id = 0, Title = "Principal", TargetType= typeof(EventoDatosVista) },
                    new EventoVistaFlyoutMenuItem { Id = 1, Title = "Tareas", TargetType= typeof(TareasVista) },
                    new EventoVistaFlyoutMenuItem { Id = 2, Title = "Compra", TargetType= typeof(CompraVista) },
                    new EventoVistaFlyoutMenuItem { Id = 3, Title = "Actividades", TargetType= typeof(ActividadesVista) },
                    new EventoVistaFlyoutMenuItem { Id = 4, Title = "Gastos", TargetType= typeof(GastosVista) },
                    new EventoVistaFlyoutMenuItem { Id = 5, Title = "Reparto de gastos", TargetType= typeof(RepartoGastosVista) },
                    new EventoVistaFlyoutMenuItem { Id = 6, Title = "Valoraciones", TargetType= typeof(ValoracionesVista) },
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}