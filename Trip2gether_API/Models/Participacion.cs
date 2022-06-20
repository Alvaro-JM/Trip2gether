using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Trip2gether_API.Models
{
    public class Participacion 
    {
        public int ID { get; set; }
        public int EventoID { get; set; }
        public int UsuarioID { get; set; }

    }
}