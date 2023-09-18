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
    public class PretsController : ControllerBase
    {
        private readonly BiblioProjContext _context;

        public PretsController(BiblioProjContext context)
        {
            _context = context;
        }

        // GET: api/Prets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pret>>> GetPret()
        {
            if(_context.Pret == null)
            {
                return NotFound();
            }
            return await _context.Pret.ToListAsync();
        }
        /// <summary>
        /// Permet de récupérer la liste des livres prêtés en ce moment.
        /// </summary>
        /// <returns></returns>
        // GET: api/Prets/EnCours
        [HttpGet("EnCours")]
        public async Task<ActionResult<IEnumerable<Pret>>> GetPretEnCours()
        {
           var PretEnCours = await _context.Pret
                .Where(p => p.DateDeRetour > DateTime.Now)
                .ToListAsync();

            return PretEnCours;
        }
        // GET: api/Prets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pret>> GetPret(int id)
        {
          if (_context.Pret == null)
          {
              return NotFound();
          }
            var pret = await _context.Pret.FindAsync(id);

            if (pret == null)
            {
                return NotFound();
            }

            return pret;
        }

        /// <summary>
        /// Marque un prêt comme retourné
        /// </summary>
        /// <param name="id">ID du prêt à marquer comme retourné.</param>
        /// <param name="pret"></param>
        /// <returns>Résultat HTTP renvoyant l\'opération :
        /// 204 NoContentFound si l'opération est un succès
        /// 404 NotFound si le prêt n\'est pas trouvé</returns>
        // PUT: api/Prets/5
        [HttpPut("Retourne/{id}")]
        public async Task<IActionResult> EmpruntRetourne(int id)
        {
            var pret = await _context.Pret.FindAsync(id);
            if (pret == null)
            {
                return NotFound();
            }
            pret.DateDeRetour = DateTime.Now;
            _context.Entry(pret).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if(!PretExists(id))
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

        // PUT: api/Prets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPret(int id, Pret pret)
        {
            if (id != pret.Id)
            {
                return BadRequest();
            }

            _context.Entry(pret).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PretExists(id))
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
        /// Permet de créer un prêt.
        /// </summary>
        /// <param name="pret"></param>
        /// <returns></returns>
        // POST: api/Prets
        [HttpPost]
        public async Task<ActionResult<Pret>> PostPret([FromBody]Pret pret)
        {
            var livre = await _context.Livre.FindAsync(pret.LivreId);
            if(livre == null) return NotFound("Livre non spécifié");
            _context.Pret.Add(pret);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPret", new { id = pret.Id }, pret);
        }

        // DELETE: api/Prets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePret(int id)
        {
            if (_context.Pret == null)
            {
                return NotFound();
            }
            var pret = await _context.Pret.FindAsync(id);
            if (pret == null)
            {
                return NotFound();
            }

            _context.Pret.Remove(pret);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PretExists(int id)
        {
            return (_context.Pret?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        private bool LivreExists(int id)
        {
            return (_context.Livre.Any(e => e.Id == id));
        }
    }
}
