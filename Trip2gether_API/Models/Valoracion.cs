using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Trip2gether_API.Models
{
    public class Valoracion
    {
        public int ID { get; set; }
        public int EventoID { get; set; }
        public int UsuarioID { get; set; }
        public int Numero { get; set; }
        public string Observacion { get; set; }
        public Usuario Usuario { get; set; }

    }
}