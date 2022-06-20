using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Trip2gether_API.Models;

namespace Trip2gether_API.Data
{
    public class Trip2getherContext : DbContext
    {
        public Trip2getherContext(DbContextOptions<Trip2getherContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Participacion> Participaciones { get; set; }
        public DbSet<Actividad> Actividades { get; set; }
        public DbSet<Articulo> Articulos { get; set; }
        public DbSet<Gasto> Gastos { get; set; }
        public DbSet<Tarea> Tareas { get; set; }
        public DbSet<Valoracion> Valoraciones { get; set; }

    }
}