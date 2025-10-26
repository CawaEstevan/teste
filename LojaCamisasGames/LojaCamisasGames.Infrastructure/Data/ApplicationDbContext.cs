using LojaCamisasGames.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LojaCamisasGames.Infrastructure.Data
{
    /// <summary>
    /// Contexto do banco de dados da aplicação
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// DbSet de camisas de games
        /// </summary>
        public DbSet<CamisaGame> CamisasGames { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração da entidade CamisaGame
            modelBuilder.Entity<CamisaGame>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.NomeTime)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Jogo)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Tamanho)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Cor)
                    .HasMaxLength(50);

                entity.Property(e => e.Preco)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)");

                entity.Property(e => e.QuantidadeEstoque)
                    .IsRequired();

                entity.Property(e => e.Disponivel)
                    .IsRequired()
                    .HasDefaultValue(true);

                entity.Property(e => e.DataCadastro)
                    .IsRequired();
            });

            // Seed de dados iniciais para demonstração
            modelBuilder.Entity<CamisaGame>().HasData(
                new CamisaGame
                {
                    Id = 1,
                    Nome = "Camisa Oficial Team Liquid 2024",
                    NomeTime = "Team Liquid",
                    Jogo = "League of Legends",
                    Tamanho = "M",
                    Cor = "Azul e Branco",
                    Preco = 149.90m,
                    QuantidadeEstoque = 50,
                    Disponivel = true,
                    DataCadastro = DateTime.Now
                },
                new CamisaGame
                {
                    Id = 2,
                    Nome = "Camisa FURIA Esports Home",
                    NomeTime = "FURIA",
                    Jogo = "CS:GO",
                    Tamanho = "G",
                    Cor = "Preto e Rosa",
                    Preco = 159.90m,
                    QuantidadeEstoque = 30,
                    Disponivel = true,
                    DataCadastro = DateTime.Now
                },
                new CamisaGame
                {
                    Id = 3,
                    Nome = "Camisa LOUD Champions",
                    NomeTime = "LOUD",
                    Jogo = "Valorant",
                    Tamanho = "M",
                    Cor = "Verde e Branco",
                    Preco = 169.90m,
                    QuantidadeEstoque = 40,
                    Disponivel = true,
                    DataCadastro = DateTime.Now
                }
            );
        }
    }
}