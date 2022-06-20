using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;


namespace Trip2gether_API.Models
{
    public class Articulo : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int id;
        private int eventoId;
        private string nombre;
        private bool seleccionado;

        public int ID
        {
            get { return id; }
            set { id = value; OnPropertyChange(); }
        }

        public int EventoID
        {
            get { return eventoId; }
            set { eventoId = value; OnPropertyChange(); }
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; OnPropertyChange(); }
        }

        public bool Seleccionado
        {
            get { return seleccionado; }
            set { seleccionado = value; OnPropertyChange(); }
        }
        
        public void OnPropertyChange([CallerMemberName] string nombre = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nombre));
        }

    }
}
