using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Trip2gether_API.Models
{
    public class Articulo
    {
        public int ID { get; set; }
        public int EventoID { get; set; }
        public string Nombre { get; set; }
        public bool Seleccionado { get; set; }

    }
}