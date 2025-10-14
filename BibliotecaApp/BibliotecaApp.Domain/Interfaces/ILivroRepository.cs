using System.Collections.Generic;
using System.Threading.Tasks;
using BibliotecaApp.Domain.Entities;

namespace BibliotecaApp.Domain.Interfaces
{
    public interface ILivroRepository
    {
        Task<IEnumerable<Livro>> GetAllAsync();
        Task<Livro> GetByIdAsync(int id);
        Task<Livro> GetByISBNAsync(string isbn);
        Task AddAsync(Livro livro);
        Task UpdateAsync(Livro livro);
        Task DeleteAsync(int id);
        Task<bool> ISBNExistsAsync(string isbn);
    }
}
