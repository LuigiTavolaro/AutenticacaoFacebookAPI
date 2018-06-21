using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using teste;
using teste.Model;

namespace teste.Controllers
{
    [Produces("application/json")]
    [Route("api/Notas")]
    public class NotasController : Controller
    {
        private readonly AspNetCoreContext _context;

        public NotasController(AspNetCoreContext context)
        {
            _context = context;
        }

        // GET: api/Notas
        [HttpGet]
        public IEnumerable<Nota> GetNotas()
        {


            return _context.Notas;
        }

        // GET: api/Notas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNota([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var nota = await _context.Notas.SingleOrDefaultAsync(m => m.Id == id);

            if (nota == null)
            {
                return NotFound();
            }

            return Ok(nota);
        }

        // PUT: api/Notas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNota([FromRoute] int id, [FromBody] Nota nota)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != nota.Id)
            {
                return BadRequest();
            }

            _context.Entry(nota).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotaExists(id))
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

        // POST: api/Notas
        [HttpPost]
        public async Task<IActionResult> PostNota([FromBody] Nota nota)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Notas.Add(nota);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNota", new { id = nota.Id }, nota);
        }

        // DELETE: api/Notas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNota([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var nota = await _context.Notas.SingleOrDefaultAsync(m => m.Id == id);
            if (nota == null)
            {
                return NotFound();
            }

            _context.Notas.Remove(nota);
            await _context.SaveChangesAsync();

            return Ok(nota);
        }

        private bool NotaExists(int id)
        {
            return _context.Notas.Any(e => e.Id == id);
        }
    }
}