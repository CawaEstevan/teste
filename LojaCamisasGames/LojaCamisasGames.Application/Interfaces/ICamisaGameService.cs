using LojaCamisasGames.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LojaCamisasGames.Application.Interfaces
{
    /// <summary>
    /// Interface do serviço de aplicação para camisas de games
    /// Define as operações de negócio disponíveis
    /// </summary>
    public interface ICamisaGameService
    {
        /// <summary>
        /// Retorna todas as camisas
        /// </summary>
        Task<IEnumerable<CamisaGameDto>> GetAllAsync();

        /// <summary>
        /// Retorna uma camisa específica pelo ID
        /// </summary>
        Task<CamisaGameDto?> GetByIdAsync(int id);

        /// <summary>
        /// Cria uma nova camisa
        /// </summary>
        Task<CamisaGameDto> CreateAsync(CamisaGameCreateDto createDto);

        /// <summary>
        /// Atualiza uma camisa existente
        /// </summary>
        Task<CamisaGameDto> UpdateAsync(CamisaGameUpdateDto updateDto);

        /// <summary>
        /// Remove uma camisa
        /// </summary>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Verifica se uma camisa existe
        /// </summary>
        Task<bool> ExistsAsync(int id);

        /// <summary>
        /// Retorna camisas de um jogo específico
        /// </summary>
        Task<IEnumerable<CamisaGameDto>> GetByJogoAsync(string jogo);

        /// <summary>
        /// Retorna apenas camisas disponíveis
        /// </summary>
        Task<IEnumerable<CamisaGameDto>> GetDisponiveisAsync();
    }
}