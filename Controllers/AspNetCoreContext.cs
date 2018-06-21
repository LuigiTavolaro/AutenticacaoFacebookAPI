using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using teste.Model;

namespace teste
{
    public class AspNetCoreContext : DbContext
    {
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Nota> Notas { get; set; }

        public AspNetCoreContext(DbContextOptions<AspNetCoreContext> options) :
            base(options)
        {
        }

          protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aluno>().HasKey(c => c.Id);

            modelBuilder.Entity<Nota>().HasKey(c => c.Id);

        }
    }
}