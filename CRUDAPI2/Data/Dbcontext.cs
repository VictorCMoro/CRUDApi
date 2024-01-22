using CRUDApi2;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CRUDApi2.Data
{
    public class AppDbContext : DbContext
    {


        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Livro> Livros { get; set; }
        public DbSet<AreaDeConhecimento> AreasDeConhecimento { get; set; }
        public DbSet<Autor> Autores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AreaDeConhecimento>()
                .HasMany(e => e.Livros)
                .WithOne(j => j.AreaDeConhecimento)
                .HasForeignKey(j => j.AreaId);



            base.OnModelCreating(modelBuilder);
        }

       
    }
}
