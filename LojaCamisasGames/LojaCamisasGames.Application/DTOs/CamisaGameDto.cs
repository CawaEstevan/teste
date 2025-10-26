using System;
using System.ComponentModel.DataAnnotations;

namespace LojaCamisasGames.Application.DTOs
{
    /// <summary>
    /// DTO para exibição de dados de camisas (usado em listagens e detalhes)
    /// </summary>
    public class CamisaGameDto
    {
        public int Id { get; set; }

        [Display(Name = "Nome da Camisa")]
        public string Nome { get; set; } = string.Empty;

        [Display(Name = "Time")]
        public string NomeTime { get; set; } = string.Empty;

        [Display(Name = "Jogo")]
        public string Jogo { get; set; } = string.Empty;

        [Display(Name = "Tamanho")]
        public string Tamanho { get; set; } = string.Empty;

        [Display(Name = "Cor")]
        public string? Cor { get; set; }

        [Display(Name = "Preço")]
        [DataType(DataType.Currency)]
        public decimal Preco { get; set; }

        [Display(Name = "Estoque")]
        public int QuantidadeEstoque { get; set; }

        [Display(Name = "Disponível")]
        public bool Disponivel { get; set; }

        [Display(Name = "Cadastrado em")]
        [DataType(DataType.DateTime)]
        public DateTime DataCadastro { get; set; }
    }
}