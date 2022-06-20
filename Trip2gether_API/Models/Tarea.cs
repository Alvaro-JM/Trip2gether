using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Trip2gether_API.Models
{
    public class Tarea 
    {
        public int ID { get; set; }
        public int EventoID { get; set; }
        public int? UsuarioID { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public bool Seleccionado { get; set; }
        public Usuario Usuario { get; set; }

    }
}