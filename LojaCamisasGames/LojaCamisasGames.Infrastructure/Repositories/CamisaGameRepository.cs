using LojaCamisasGames.Domain.Entities;
using LojaCamisasGames.Domain.Interfaces;
using LojaCamisasGames.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaCamisasGames.Infrastructure.Repositories
{
    /// <summary>
    /// Implementação do repositório de camisas usando Entity Framework Core
    /// </summary>
    public class CamisaGameRepository : ICamisaGameRepository
    {
        private readonly ApplicationDbContext _context;

        public CamisaGameRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CamisaGame>> GetAllAsync()
        {
            return await _context.CamisasGames
                .OrderByDescending(c => c.DataCadastro)
                .ToListAsync();
        }

        public async Task<CamisaGame?> GetByIdAsync(int id)
        {
            return await _context.CamisasGames
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<CamisaGame> AddAsync(CamisaGame camisaGame)
        {
            await _context.CamisasGames.AddAsync(camisaGame);
            await _context.SaveChangesAsync();
            return camisaGame;
        }

        public async Task<CamisaGame> UpdateAsync(CamisaGame camisaGame)
        {
            _context.CamisasGames.Update(camisaGame);
            await _context.SaveChangesAsync();
            return camisaGame;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var camisa = await GetByIdAsync(id);
            
            if (camisa == null)
            {
                return false;
            }

            _context.CamisasGames.Remove(camisa);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.CamisasGames
                .AnyAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<CamisaGame>> GetByJogoAsync(string jogo)
        {
            return await _context.CamisasGames
                .Where(c => c.Jogo.Contains(jogo))
                .OrderBy(c => c.NomeTime)
                .ToListAsync();
        }

        public async Task<IEnumerable<CamisaGame>> GetDisponiveisAsync()
        {
            return await _context.CamisasGames
                .Where(c => c.Disponivel && c.QuantidadeEstoque > 0)
                .OrderByDescending(c => c.DataCadastro)
                .ToListAsync();
        }
    }
}