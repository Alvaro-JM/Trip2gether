using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trip2gether_API.Data;
using Trip2gether_API.Models;

namespace Trip2gether_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventosController : ControllerBase
    {
        private readonly Trip2getherContext _context;

        public EventosController(Trip2getherContext context)
        {
            _context = context;
        }

        // GET: api/Eventos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Evento>>> GetEventos()
        {
            return await _context.Eventos.ToListAsync();
        }

        // GET: api/Eventos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Evento>> GetEvento(int id)
        {
            var evento = await _context.Eventos.FindAsync(id);

            if (evento == null)
            {
                return NotFound();
            }

            return evento;
        }

        // GET: api/Eventos/5/Actividades
        [HttpGet("{id}/Actividades")]
        public async Task<ActionResult<IEnumerable<Actividad>>> GetActividadesFromEvento(int id)
        {
            try
            {
                IQueryable<Actividad> query = _context.Actividades;

                if (id >= 1)
                {
                    query = query.Where(a => a.EventoID == id);

                    var result = await query.ToListAsync();

                    if (result.Any())
                    {
                        return Ok(result);
                    }
                }

                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        // GET: api/Eventos/5/Articulos
        [HttpGet("{id}/Articulos")]
        public async Task<ActionResult<IEnumerable<Articulo>>> GetArticulosFromEvento(int id)
        {
            try
            {
                IQueryable<Articulo> query = _context.Articulos;

                if (id >= 1)
                {
                    query = query.Where(a => a.EventoID == id);

                    var result = await query.ToListAsync();

                    if (result.Any())
                    {
                        return Ok(result);
                    }
                }

                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        // GET: api/Eventos/5/Gastos
        [HttpGet("{id}/Gastos")]
        public async Task<ActionResult<IEnumerable<Gasto>>> GetGastosFromEvento(int id)
        {
            try
            {
                if (id >= 1)
                {
                    IQueryable<Gasto> query =
                        from g in _context.Gastos
                        join u in _context.Usuarios
                        on g.UsuarioID equals u.ID
                        where g.EventoID == id
                        select new Gasto()
                        {
                            ID = g.ID,
                            EventoID = g.EventoID,
                            UsuarioID = g.UsuarioID,
                            Cuantia = g.Cuantia,
                            Concepto = g.Concepto,
                            Usuario = u
                        };        

                    var result = await query.ToListAsync();

                    if (result.Any())
                    {
                        return Ok(result);
                    }
                }

                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        // GET: api/Eventos/5/Tareas
        [HttpGet("{id}/Tareas")]
        public async Task<ActionResult<IEnumerable<Tarea>>> GetTareasFromEvento(int id)
        {
            try
            {
                IQueryable<Tarea> query = _context.Tareas;

                if (id >= 1)
                {
                    query = query.Where(a => a.EventoID == id);

                    var result = await query.ToListAsync();

                    if (result.Any())
                    {
                        foreach (var item in result)
                        {
                            if (item.UsuarioID != null)
                            {
                                var usuario = await _context.Usuarios.FindAsync(item.UsuarioID);

                                item.Usuario = usuario;
                            }
                        }

                        return Ok(result);
                    }
                }

                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }

        }

        // GET: api/Eventos/5/Valoraciones
        [HttpGet("{id}/Valoraciones")]
        public async Task<ActionResult<IEnumerable<Valoracion>>> GetValoracionesFromEvento(int id)
        {
            try
            {
                if (id >= 1)
                {
                    IQueryable<Valoracion> query =
                        from v in _context.Valoraciones
                        join u in _context.Usuarios
                        on v.UsuarioID equals u.ID
                        where v.EventoID == id
                        select new Valoracion()
                        {
                            ID = v.ID,
                            EventoID = v.EventoID,
                            UsuarioID = v.UsuarioID,
                            Numero = v.Numero,
                            Observacion = v.Observacion,
                            Usuario = u
                        };

                    var result = await query.ToListAsync();

                    if (result.Any())
                    {
                        return Ok(result);
                    }
                }

                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        // GET: api/Eventos/5/Usuarios
        [HttpGet("{id}/Usuarios")]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuariosFromEvento(int id)
        {
            try
            {
                if (id >= 1)
                {
                    IQueryable<Usuario> query = _context.Usuarios
                        .Join(_context.Participaciones,
                            u => u.ID,
                            p => p.UsuarioID,
                            (u, p) => new { u, p })
                        .Where(x => x.p.EventoID == id)
                        .Select(x => x.u);

                    var result = await query.ToListAsync();

                    if (result.Any())
                    {
                        return Ok(result);
                    }
                }

                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        // GET: api/Eventos/
        [HttpGet("name={nombre}/pass={password}")]
        public async Task<ActionResult<IEnumerable<Evento>>> GetEventoByNombrePassword(string nombre, string password)
        {
            IQueryable<Evento> query = _context.Eventos
                        .Where(u => u.Nombre == nombre && u.Password == password)
                        .Select(u => u);

            var result = await query.FirstOrDefaultAsync();

            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        // PUT: api/Eventos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvento(int id, Evento evento)
        {
            if (id != evento.ID)
            {
                return BadRequest();
            }

            _context.Entry(evento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Eventos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Evento>> PostEvento(Evento evento)
        {
            _context.Eventos.Add(evento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEvento", new { id = evento.ID }, evento);
        }

        // DELETE: api/Eventos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvento(int id)
        {
            var evento = await _context.Eventos.FindAsync(id);
            if (evento == null)
            {
                return NotFound();
            }

            _context.Eventos.Remove(evento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EventoExists(int id)
        {
            return _context.Eventos.Any(e => e.ID == id);
        }
    }
}
