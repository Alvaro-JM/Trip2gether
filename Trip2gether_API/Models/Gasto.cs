using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Trip2gether_API.Models
{
    public class Gasto
    {
        public int ID { get; set; }
        public int EventoID { get; set; }
        public int UsuarioID { get; set; }
        public double Cuantia { get; set; }
        public string Concepto { get; set; }
        public Usuario Usuario { get; set; }

    }
}