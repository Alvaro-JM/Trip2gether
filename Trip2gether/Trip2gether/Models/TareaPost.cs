using System;
using System.Collections.Generic;
using System.Text;

namespace Trip2gether.Models
{
    class TareaPost 
    {
        private int eventoId;
        private string titulo;
        private string descripcion;

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

    }
}