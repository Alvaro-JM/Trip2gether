using System;
using System.Collections.Generic;
using System.Text;
using Trip2gether_API.Models;

namespace Trip2gether.Models
{
    class UsuarioPago : Usuario
    {
        public double Deuda { get; set; }

        public void Recibe(double pago)
        {
            Deuda += pago;
        }

        public void Paga(double pago)
        {
            Deuda -= pago;
        }

    }
}
