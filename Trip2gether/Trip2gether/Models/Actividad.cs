using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;


namespace Trip2gether_API.Models
{
    public class Actividad : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int id;
        private int eventoId;
        private string titulo;
        private string descripcion;
        private DateTime? fechaHora;

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

        public string Titulo
        {
            get { return titulo; }
            set { titulo = value; OnPropertyChange(); }
        }        

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; OnPropertyChange(); }
        }        

        public DateTime? FechaHora
        {
            get { return fechaHora; }
            set { fechaHora = value; OnPropertyChange(); }
        }
        public void OnPropertyChange([CallerMemberName] string nombre = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nombre));
        }

    }
}
