using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trip2gether_API.Models;

namespace Trip2gether.Vistas
{
    public class EventoVistaFlyoutMenuItem
    {
        public EventoVistaFlyoutMenuItem()
        {
            TargetType = typeof(EventoVistaFlyoutMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public Type TargetType { get; set; }

    }
}