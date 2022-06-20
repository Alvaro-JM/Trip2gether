using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;


namespace Trip2gether_API.Models
{
    public class Valoracion : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int id;
        private int eventoId;
        private int usuarioId;
        private int numero;
        private string observacion;
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

        public int Numero
        {
            get { return numero; }
            set { numero = value; OnPropertyChange(); }
        }

        public string Observacion
        {
            get { return observacion; }
            set { observacion = value; OnPropertyChange(); }
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