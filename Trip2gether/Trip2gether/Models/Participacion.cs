using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;


namespace Trip2gether_API.Models
{
    public class Participacion : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int id;
        private int eventoId;
        private int usuarioId;

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

        public void OnPropertyChange([CallerMemberName] string nombre = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nombre));
        }

    }
}