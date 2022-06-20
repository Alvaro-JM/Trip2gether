using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;


namespace Trip2gether_API.Models
{
    public class Actividad
    {
        public int ID { get; set; }
        public int EventoID { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:g}", ApplyFormatInEditMode = true)]
        public DateTime? FechaHora { get; set; }

    }
}
