using Microsoft.EntityFrameworkCore;
using BibliotecaApp.Domain.Entities;

namespace BibliotecaApp.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Livro> Livros { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Livro>(entity =>
            {
                entity.HasKey(e => e.Id);
                
                entity.Property(e => e.Titulo)
                    .IsRequired()
                    .HasMaxLength(200);
                
                entity.Property(e => e.Autor)
                    .IsRequired()
                    .HasMaxLength(150);
                
                entity.Property(e => e.ISBN)
                    .IsRequired()
                    .HasMaxLength(13);
                
                entity.HasIndex(e => e.ISBN)
                    .IsUnique();
                
                entity.Property(e => e.AnoPublicacao)
                    .IsRequired();
                
                entity.Property(e => e.Disponivel)
                    .IsRequired();
                
                entity.Property(e => e.DataCriacao)
                    .IsRequired();
            });
        }
    }
}
