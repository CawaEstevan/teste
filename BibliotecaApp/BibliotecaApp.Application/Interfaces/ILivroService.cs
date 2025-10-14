using System.Collections.Generic;
using System.Threading.Tasks;
using BibliotecaApp.Application.DTOs;

namespace BibliotecaApp.Application.Interfaces
{
    public interface ILivroService
    {
        Task<IEnumerable<LivroDto>> GetAllLivrosAsync();
        Task<LivroDto> GetLivroByIdAsync(int id);
        Task<LivroDto> CreateLivroAsync(LivroCreateDto livroDto);
        Task UpdateLivroAsync(LivroUpdateDto livroDto);
        Task DeleteLivroAsync(int id);
        Task<bool> ISBNExistsAsync(string isbn, int? livroId = null);
    }
}
