using System;
using System.Collections.Generic;
using System.Text;
using Trip2gether_API.Models;

namespace Trip2gether.Models
{
    class Pago
    {
        public Usuario Pagador { get; set; }
        public Usuario Recibidor { get; set; }
        public double Cuantia { get; set; }

        public Pago(Usuario pagador, Usuario recibidor, double cuantia)
        {
            Pagador = pagador;
            Recibidor = recibidor;
            Cuantia = cuantia;
        }
    }
}
