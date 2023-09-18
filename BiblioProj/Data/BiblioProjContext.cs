using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BiblioProj.Models;

namespace BiblioProj.Data
{
    public class BiblioProjContext : DbContext
    {
        public BiblioProjContext (DbContextOptions<BiblioProjContext> options)
            : base(options)
        {
        }

        public DbSet<Auteur> Auteur { get; set; } = default!;

        public DbSet<Livre> Livre { get; set; } = default!;

        public DbSet<Pret> Pret { get; set; } = default!;
    }
}
