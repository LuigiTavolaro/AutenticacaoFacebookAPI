using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using teste;
using teste.Model;
using FacebookConnection;

namespace teste.Controllers
{
    [Produces("application/json")]
    [Route("api/Alunos")]
    public class AlunosController : Controller
    {
        private readonly AspNetCoreContext _context;
        public facebookToken fb { get; set; }

        public AlunosController(AspNetCoreContext context)
        {
            _context = context;
            fb = new facebookToken();
        }

        // GET: api/Alunoes
        [HttpGet]
        public IEnumerable<Aluno> GetAlunos()
        {
            return _context.Alunos.Include(o=>o.Notas);
        }

        // GET: api/Alunoes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAluno([FromRoute] int id)
        {
            if (!ModelState.IsValid & fb.ValidaUsuarioFacebook())
                return BadRequest(ModelState);

            var aluno = await _context.Alunos.Include(o => o.Notas).SingleOrDefaultAsync(m => m.Id == id);

            if (aluno == null)
                return NotFound();

            return Ok(aluno);
        }
        // PUT: api/Alunoes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAluno([FromRoute] int id, Aluno aluno)
        {
            if (!ModelState.IsValid & fb.ValidaUsuarioFacebook())
                return BadRequest(ModelState);

            if (id != aluno.Id)
                return BadRequest();

            _context.Entry(aluno).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlunoExists(id))
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

        // POST: api/Alunoes
        [HttpPost]
        public async Task<IActionResult> PostAluno(Aluno aluno)
        {
            if (!ModelState.IsValid & fb.ValidaUsuarioFacebook())
            {
                return BadRequest(ModelState);
            }

            _context.Alunos.Add(aluno);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAluno", new { id = aluno.Id }, aluno);
        }

        // DELETE: api/Alunoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAluno([FromRoute] int id)
        {
            if (!ModelState.IsValid & fb.ValidaUsuarioFacebook())
            {
                return BadRequest(ModelState);
            }

            var aluno = await _context.Alunos.SingleOrDefaultAsync(m => m.Id == id);
            if (aluno == null)
            {
                return NotFound();
            }

            _context.Alunos.Remove(aluno);
            await _context.SaveChangesAsync();

            return Ok(aluno);
        }

        private bool AlunoExists(int id)
        {
            return _context.Alunos.Any(e => e.Id == id);
        }
    }
}