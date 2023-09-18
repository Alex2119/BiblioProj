using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BiblioProj.Data;
using BiblioProj.Models;

namespace BiblioProj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuteursController : ControllerBase
    {
        private readonly BiblioProjContext _context;

        public AuteursController(BiblioProjContext context)
        {
            _context = context;
        }

        // GET: api/Auteurs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Auteur>>> GetAuteur()
        {
          if (_context.Auteur == null)
          {
              return NotFound();
          }
            return await _context.Auteur.ToListAsync();
        }

        // GET: api/Auteurs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Auteur>> GetAuteur(int id)
        {
          if (_context.Auteur == null)
          {
              return NotFound();
          }
            var auteur = await _context.Auteur.FindAsync(id);

            if (auteur == null)
            {
                return NotFound();
            }

            return auteur;
        }

        // PUT: api/Auteurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuteur(int id, Auteur auteur)
        {
            if (id != auteur.Id)
            {
                return BadRequest();
            }

            _context.Entry(auteur).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuteurExists(id))
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
        /// <summary>
        /// Permet de créer un Auteur en base de données.
        /// </summary>
        /// <param name="auteur"></param>
        /// <returns></returns>
        // POST: api/Auteurs
        [HttpPost]
        public async Task<ActionResult<Auteur>> PostAuteur(Auteur auteur)
        {
          if (_context.Auteur == null)
          {
              return BadRequest("Les données de l'auteur de sont pas fournies !");
          }
            _context.Auteur.Add(auteur);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAuteur", new { id = auteur.Id }, auteur);
        }

        // DELETE: api/Auteurs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuteur(int id)
        {
            if (_context.Auteur == null)
            {
                return NotFound();
            }
            var auteur = await _context.Auteur.FindAsync(id);
            if (auteur == null)
            {
                return NotFound();
            }

            _context.Auteur.Remove(auteur);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AuteurExists(int id)
        {
            return (_context.Auteur?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
