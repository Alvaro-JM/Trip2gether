using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;


namespace Trip2gether_API.Models
{
    public class Tarea : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int id;
        private int eventoId;
        private int usuarioId;
        private string titulo;
        private string descripcion;
        private bool seleccionado;
        private Usuario usuario;

        public int? ID
        {
            get { return id; }
            set { id = (int)value; OnPropertyChange(); }
        }

        public int EventoID
        {
            get { return eventoId; }
            set { eventoId = value; OnPropertyChange(); }
        }

        public int? UsuarioID
        {
            get { return usuarioId; }
            set { usuarioId = (int)value; OnPropertyChange(); }
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

        public bool Seleccionado
        {
            get { return seleccionado; }
            set { seleccionado = value; OnPropertyChange(); }
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