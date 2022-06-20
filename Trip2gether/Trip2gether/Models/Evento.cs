using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace Trip2gether_API.Models
{
    public class Evento : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int id;
        private string nombre;
        private string password;
        private string destino;
        private string alojamiento;
        private DateTime? fechaInicio;
        private DateTime? fechaFin;

        public int ID
        {
            get { return id; }
            set { id = value; OnPropertyChange(); }
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; OnPropertyChange(); }
        }

        public string Password
        {
            get { return password; }
            set { password = value; OnPropertyChange(); }
        }

        public string Destino
        {
            get { return destino; }
            set { destino = value; OnPropertyChange(); }
        }

        public string Alojamiento
        {
            get { return alojamiento; }
            set { alojamiento = value; OnPropertyChange(); }
        }

        public DateTime? FechaInicio
        {
            get { return fechaInicio; }
            set { fechaInicio = value; OnPropertyChange(); }
        }

        public DateTime? FechaFin
        {
            get { return fechaFin; }
            set { fechaFin = value; OnPropertyChange(); }
        }

        public void OnPropertyChange([CallerMemberName] string nombre = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nombre));
        }

    }
}