using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Trip2gether_API.Models
{
    public class Usuario
    {
        public int ID { get; set; }
        public string Mail { get; set; }
        public string Nombre { get; set; }
        public string Password { get; set; }
        public string Imagen { get; set; }

    }
}