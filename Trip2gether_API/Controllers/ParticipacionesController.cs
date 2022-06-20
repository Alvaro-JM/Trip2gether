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
    public class ParticipacionesController : ControllerBase
    {
        private readonly Trip2getherContext _context;

        public ParticipacionesController(Trip2getherContext context)
        {
            _context = context;
        }

        // GET: api/Participaciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Participacion>>> GetParticipaciones()
        {
            return await _context.Participaciones.ToListAsync();
        }

        // GET: api/Participaciones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Participacion>> GetParticipacion(int id)
        {
            var participacion = await _context.Participaciones.FindAsync(id);

            if (participacion == null)
            {
                return NotFound();
            }

            return participacion;
        }

        // PUT: api/Participaciones/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParticipacion(int id, Participacion participacion)
        {
            if (id != participacion.ID)
            {
                return BadRequest();
            }

            _context.Entry(participacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParticipacionExists(id))
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

        // POST: api/Participaciones
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Participacion>> PostParticipacion(Participacion participacion)
        {
            _context.Participaciones.Add(participacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetParticipacion", new { id = participacion.ID }, participacion);
        }

        // DELETE: api/Participaciones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParticipacion(int id)
        {
            var participacion = await _context.Participaciones.FindAsync(id);
            if (participacion == null)
            {
                return NotFound();
            }

            _context.Participaciones.Remove(participacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ParticipacionExists(int id)
        {
            return _context.Participaciones.Any(e => e.ID == id);
        }
    }
}
