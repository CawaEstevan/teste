using LojaCamisasGames.Application.DTOs;
using LojaCamisasGames.Application.Interfaces;
using LojaCamisasGames.Domain.Entities;
using LojaCamisasGames.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaCamisasGames.Application.Services
{
    /// <summary>
    /// Serviço de aplicação que implementa a lógica de negócio para camisas
    /// Faz a ponte entre a camada de apresentação e o domínio
    /// </summary>
    public class CamisaGameService : ICamisaGameService
    {
        private readonly ICamisaGameRepository _repository;

        public CamisaGameService(ICamisaGameRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CamisaGameDto>> GetAllAsync()
        {
            var camisas = await _repository.GetAllAsync();
            return camisas.Select(MapToDto);
        }

        public async Task<CamisaGameDto?> GetByIdAsync(int id)
        {
            var camisa = await _repository.GetByIdAsync(id);
            return camisa != null ? MapToDto(camisa) : null;
        }

        public async Task<CamisaGameDto> CreateAsync(CamisaGameCreateDto createDto)
        {
            var camisa = new CamisaGame
            {
                Nome = createDto.Nome,
                NomeTime = createDto.NomeTime,
                Jogo = createDto.Jogo,
                Tamanho = createDto.Tamanho,
                Cor = createDto.Cor,
                Preco = createDto.Preco,
                QuantidadeEstoque = createDto.QuantidadeEstoque,
                Disponivel = createDto.Disponivel,
                DataCadastro = DateTime.Now
            };

            var camisaCriada = await _repository.AddAsync(camisa);
            return MapToDto(camisaCriada);
        }

        public async Task<CamisaGameDto> UpdateAsync(CamisaGameUpdateDto updateDto)
        {
            var camisaExistente = await _repository.GetByIdAsync(updateDto.Id);
            
            if (camisaExistente == null)
            {
                throw new InvalidOperationException($"Camisa com ID {updateDto.Id} não encontrada.");
            }

            camisaExistente.Nome = updateDto.Nome;
            camisaExistente.NomeTime = updateDto.NomeTime;
            camisaExistente.Jogo = updateDto.Jogo;
            camisaExistente.Tamanho = updateDto.Tamanho;
            camisaExistente.Cor = updateDto.Cor;
            camisaExistente.Preco = updateDto.Preco;
            camisaExistente.QuantidadeEstoque = updateDto.QuantidadeEstoque;
            camisaExistente.Disponivel = updateDto.Disponivel;

            var camisaAtualizada = await _repository.UpdateAsync(camisaExistente);
            return MapToDto(camisaAtualizada);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _repository.ExistsAsync(id);
        }

        public async Task<IEnumerable<CamisaGameDto>> GetByJogoAsync(string jogo)
        {
            var camisas = await _repository.GetByJogoAsync(jogo);
            return camisas.Select(MapToDto);
        }

        public async Task<IEnumerable<CamisaGameDto>> GetDisponiveisAsync()
        {
            var camisas = await _repository.GetDisponiveisAsync();
            return camisas.Select(MapToDto);
        }

        /// <summary>
        /// Mapeia a entidade de domínio para o DTO
        /// </summary>
        private static CamisaGameDto MapToDto(CamisaGame camisa)
        {
            return new CamisaGameDto
            {
                Id = camisa.Id,
                Nome = camisa.Nome,
                NomeTime = camisa.NomeTime,
                Jogo = camisa.Jogo,
                Tamanho = camisa.Tamanho,
                Cor = camisa.Cor,
                Preco = camisa.Preco,
                QuantidadeEstoque = camisa.QuantidadeEstoque,
                Disponivel = camisa.Disponivel,
                DataCadastro = camisa.DataCadastro
            };
        }
    }
}