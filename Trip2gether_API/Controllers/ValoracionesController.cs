﻿using System;
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
    public class ValoracionesController : ControllerBase
    {
        private readonly Trip2getherContext _context;

        public ValoracionesController(Trip2getherContext context)
        {
            _context = context;
        }

        // GET: api/Valoraciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Valoracion>>> GetValoraciones()
        {
            return await _context.Valoraciones.ToListAsync();
        }

        // GET: api/Valoraciones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Valoracion>> GetValoracion(int id)
        {
            var valoracion = await _context.Valoraciones.FindAsync(id);

            if (valoracion == null)
            {
                return NotFound();
            }

            return valoracion;
        }

        // PUT: api/Valoraciones/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutValoracion(int id, Valoracion valoracion)
        {
            if (id != valoracion.ID)
            {
                return BadRequest();
            }

            _context.Entry(valoracion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ValoracionExists(id))
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

        // POST: api/Valoraciones
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Valoracion>> PostValoracion(Valoracion valoracion)
        {
            _context.Valoraciones.Add(valoracion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetValoracion", new { id = valoracion.ID }, valoracion);
        }

        // DELETE: api/Valoraciones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteValoracion(int id)
        {
            var valoracion = await _context.Valoraciones.FindAsync(id);
            if (valoracion == null)
            {
                return NotFound();
            }

            _context.Valoraciones.Remove(valoracion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ValoracionExists(int id)
        {
            return _context.Valoraciones.Any(e => e.ID == id);
        }
    }
}
