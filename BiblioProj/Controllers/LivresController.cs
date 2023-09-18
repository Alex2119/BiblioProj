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
    public class LivresController : ControllerBase
    {
        private readonly BiblioProjContext _context;

        public LivresController(BiblioProjContext context)
        {
            _context = context;
        }

        // GET: api/Livres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Livre>>> GetLivre()
        {
          if (_context.Livre == null)
          {
              return NotFound();
          }
            return await _context.Livre.ToListAsync();
        }
        /// <summary>
        /// Récupère une liste de  livres avec es détails des auteurs.
        /// </summary>
        /// <returns></returns>
        // GET: api/Livres/DetailsAuteur
        [HttpGet("AutorDetails")]
        public async Task<ActionResult<IEnumerable<LivreDTO>>> GetLivresWithAutorDetails()
        {
            var livres = await _context.Livre
                .Include(l => l.Auteur)
                .Select(l => new LivreDTO
                {
                    Id=l.Id,
                    Titre = l.Titre,
                    AnneeDePublication = l.AnneeDePublication,
                    AuteurNom = l.Auteur.Nom,
                    AuteurPrenom = l.Auteur.Prenom
                })
                .ToListAsync(); 
            
            return livres;
        }

        // GET: api/Livres/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Livre>> GetLivre(int id)
        {
          if (_context.Livre == null)
          {
              return NotFound();
          }
            var livre = await _context.Livre.FindAsync(id);

            if (livre == null)
            {
                return NotFound();
            }

            return livre;
        }

        // PUT: api/Livres/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLivre(int id, Livre livre)
        {
            if (id != livre.Id)
            {
                return BadRequest();
            }

            _context.Entry(livre).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LivreExists(id))
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
        /// Permet de créer un livre en fournissant son nom.
        /// </summary>
        /// <param name="livre"></param>
        /// <returns></returns>
        // POST: api/Livres
        [HttpPost]
        public async Task<ActionResult<Livre>> PostLivre([FromBody] Livre livre)
        {
            var auteur = await _context.Auteur.FindAsync(livre.AuteurId);
            if(auteur == null) return NotFound("Auteur non spécifié");
            _context.Livre.Add(livre);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLivre", new { id = livre.Id }, livre);
        }

        // DELETE: api/Livres/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLivre(int id)
        {
            if (_context.Livre == null)
            {
                return NotFound();
            }
            var livre = await _context.Livre.FindAsync(id);
            if (livre == null)
            {
                return NotFound();
            }

            _context.Livre.Remove(livre);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LivreExists(int id)
        {
            return (_context.Livre?.Any(e => e.Id == id)).GetValueOrDefault();
        }
       
    }
}
