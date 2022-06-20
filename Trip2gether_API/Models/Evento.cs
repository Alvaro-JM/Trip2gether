using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Trip2gether_API.Models
{
    public class Evento
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Password { get; set; }
        public string Destino { get; set; }
        public string Alojamiento { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime? FechaInicio { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime? FechaFin { get; set; }

    }
}