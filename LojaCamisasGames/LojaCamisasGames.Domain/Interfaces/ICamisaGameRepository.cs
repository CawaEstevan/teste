using LojaCamisasGames.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LojaCamisasGames.Domain.Interfaces
{
    /// <summary>
    /// Interface do repositório de camisas de games - Define o contrato de persistência
    /// </summary>
    public interface ICamisaGameRepository
    {
        /// <summary>
        /// Retorna todas as camisas cadastradas
        /// </summary>
        Task<IEnumerable<CamisaGame>> GetAllAsync();

        /// <summary>
        /// Retorna uma camisa específica pelo ID
        /// </summary>
        Task<CamisaGame?> GetByIdAsync(int id);

        /// <summary>
        /// Adiciona uma nova camisa ao banco de dados
        /// </summary>
        Task<CamisaGame> AddAsync(CamisaGame camisaGame);

        /// <summary>
        /// Atualiza os dados de uma camisa existente
        /// </summary>
        Task<CamisaGame> UpdateAsync(CamisaGame camisaGame);

        /// <summary>
        /// Remove uma camisa do banco de dados
        /// </summary>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Verifica se uma camisa existe pelo ID
        /// </summary>
        Task<bool> ExistsAsync(int id);

        /// <summary>
        /// Retorna camisas filtradas por jogo
        /// </summary>
        Task<IEnumerable<CamisaGame>> GetByJogoAsync(string jogo);

        /// <summary>
        /// Retorna camisas disponíveis para venda
        /// </summary>
        Task<IEnumerable<CamisaGame>> GetDisponiveisAsync();
    }
}