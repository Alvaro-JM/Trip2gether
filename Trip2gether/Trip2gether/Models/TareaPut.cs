using System;
using System.Collections.Generic;
using System.Text;

namespace Trip2gether.Models
{
    class TareaPut
    {
        private int id;
        private int eventoId;
        private string titulo;
        private string descripcion;
        private bool seleccionado;

        public int? ID
        {
            get { return id; }
            set { id = (int)value; }
        }

        public int EventoID
        {
            get { return eventoId; }
            set { eventoId = value; }
        }

        public string Titulo
        {
            get { return titulo; }
            set { titulo = value; }
        }

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        public bool Seleccionado
        {
            get { return seleccionado; }
            set { seleccionado = value; }
        }

    }
}