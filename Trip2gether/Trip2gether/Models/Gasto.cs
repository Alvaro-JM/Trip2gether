using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;


namespace Trip2gether_API.Models
{
    public class Gasto : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int id;
        private int eventoId;
        private int usuarioId;
        private double cuantia;
        private string concepto;
        private Usuario usuario;

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

        public int UsuarioID
        {
            get { return usuarioId; }
            set { usuarioId = value; OnPropertyChange(); }
        }

        public double Cuantia
        {
            get { return cuantia; }
            set { cuantia = value; OnPropertyChange(); }
        }

        public string Concepto
        {
            get { return concepto; }
            set { concepto = value; OnPropertyChange(); }
        }

        public Usuario Usuario
        {
            get { return usuario; }
            set { usuario = value; OnPropertyChange(); }
        }

        public void OnPropertyChange([CallerMemberName] string nombre = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nombre));
        }

    }
}