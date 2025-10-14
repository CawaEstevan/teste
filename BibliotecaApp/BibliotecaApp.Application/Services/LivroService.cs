using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BibliotecaApp.Application.DTOs;
using BibliotecaApp.Application.Interfaces;
using BibliotecaApp.Domain.Entities;
using BibliotecaApp.Domain.Interfaces;

namespace BibliotecaApp.Application.Services
{
    public class LivroService : ILivroService
    {
        private readonly ILivroRepository _livroRepository;

        public LivroService(ILivroRepository livroRepository)
        {
            _livroRepository = livroRepository;
        }

        public async Task<IEnumerable<LivroDto>> GetAllLivrosAsync()
        {
            var livros = await _livroRepository.GetAllAsync();
            return livros.Select(MapToDto);
        }

        public async Task<LivroDto> GetLivroByIdAsync(int id)
        {
            var livro = await _livroRepository.GetByIdAsync(id);
            return livro != null ? MapToDto(livro) : null;
        }

        public async Task<LivroDto> CreateLivroAsync(LivroCreateDto livroDto)
        {
            // Validação de ISBN único
            if (await _livroRepository.ISBNExistsAsync(livroDto.ISBN))
            {
                throw new InvalidOperationException("Já existe um livro com este ISBN");
            }

            var livro = new Livro(
                livroDto.Titulo,
                livroDto.Autor,
                livroDto.ISBN,
                livroDto.AnoPublicacao
            );

            await _livroRepository.AddAsync(livro);
            return MapToDto(livro);
        }

        public async Task UpdateLivroAsync(LivroUpdateDto livroDto)
        {
            var livro = await _livroRepository.GetByIdAsync(livroDto.Id);
            
            if (livro == null)
            {
                throw new KeyNotFoundException("Livro não encontrado");
            }

            // Validação de ISBN único (exceto o próprio livro)
            var livroComMesmoISBN = await _livroRepository.GetByISBNAsync(livroDto.ISBN);
            if (livroComMesmoISBN != null && livroComMesmoISBN.Id != livroDto.Id)
            {
                throw new InvalidOperationException("Já existe outro livro com este ISBN");
            }

            livro.Atualizar(
                livroDto.Titulo,
                livroDto.Autor,
                livroDto.ISBN,
                livroDto.AnoPublicacao
            );

            if (livroDto.Disponivel)
                livro.MarcarComoDisponivel();
            else
                livro.MarcarComoIndisponivel();

            await _livroRepository.UpdateAsync(livro);
        }

        public async Task DeleteLivroAsync(int id)
        {
            var livro = await _livroRepository.GetByIdAsync(id);
            
            if (livro == null)
            {
                throw new KeyNotFoundException("Livro não encontrado");
            }

            await _livroRepository.DeleteAsync(id);
        }

        public async Task<bool> ISBNExistsAsync(string isbn, int? livroId = null)
        {
            var livro = await _livroRepository.GetByISBNAsync(isbn);
            
            if (livro == null)
                return false;
            
            if (livroId.HasValue && livro.Id == livroId.Value)
                return false;
            
            return true;
        }

        private LivroDto MapToDto(Livro livro)
        {
            return new LivroDto
            {
                Id = livro.Id,
                Titulo = livro.Titulo,
                Autor = livro.Autor,
                ISBN = livro.ISBN,
                AnoPublicacao = livro.AnoPublicacao,
                Disponivel = livro.Disponivel,
                DataCriacao = livro.DataCriacao
            };
        }
    }
}
