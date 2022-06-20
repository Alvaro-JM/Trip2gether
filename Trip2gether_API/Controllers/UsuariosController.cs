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
    public class UsuariosController : ControllerBase
    {
        private readonly Trip2getherContext _context;

        public UsuariosController(Trip2getherContext context)
        {
            _context = context;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        // GET: api/Usuarios/
        [Route("user={nombre}/pass={password}")]
        [HttpGet]
        public async Task<ActionResult<Usuario>> GetUsuarioByNombreClave(string nombre, string password)
        {
            IQueryable<Usuario> query = _context.Usuarios
                        .Where(u => u.Nombre == nombre && u.Password == password)
                        .Select(u => u);

            var result = await query.FirstOrDefaultAsync();

            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        // GET: api/Usuarios/5/Eventos
        [HttpGet("{id}/Eventos")]
        public async Task<ActionResult<IEnumerable<Evento>>> GetEventoFromUsuario(int id)
        {
            try
            {
                if (id >= 1)
                {
                    IQueryable<Evento> query = _context.Eventos
                        .Join(_context.Participaciones,
                            e => e.ID,
                            p => p.EventoID,
                            (e, p) => new { e, p })
                        .Where(x => x.p.UsuarioID == id)
                        .Select(x => x.e);

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

        // PUT: api/Usuarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
        {
            if (id != usuario.ID)
            {
                return BadRequest();
            }

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
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

        // POST: api/Usuarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuario", new { id = usuario.ID }, usuario);
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.ID == id);
        }
    }
}
