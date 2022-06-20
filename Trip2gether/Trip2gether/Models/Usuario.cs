using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;


namespace Trip2gether_API.Models
{
    public class Usuario : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int id;
        private string mail;
        private string nombre;
        private string password;
        private string imagen;

        public int ID
        {
            get { return id; }
            set { id = value; OnPropertyChange(); }
        }

        public string Mail
        {
            get { return mail; }
            set { mail = value; OnPropertyChange(); }
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

        public string Imagen
        {
            get { return imagen; }
            set { imagen = value; OnPropertyChange(); }
        }

        public void OnPropertyChange([CallerMemberName] string nombre = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nombre));
        }

    }
}