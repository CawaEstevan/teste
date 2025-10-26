using System;
using System.ComponentModel.DataAnnotations;

namespace LojaCamisasGames.Domain.Entities
{
    /// <summary>
    /// Entidade de domínio que representa uma camisa de time de e-sports
    /// </summary>
    public class CamisaGame
    {
        /// <summary>
        /// Identificador único da camisa
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Nome comercial da camisa
        /// </summary>
        [Required(ErrorMessage = "O nome da camisa é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres")]
        public string Nome { get; set; } = string.Empty;

        /// <summary>
        /// Nome do time de e-sports
        /// </summary>
        [Required(ErrorMessage = "O nome do time é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome do time deve ter no máximo 100 caracteres")]
        [Display(Name = "Nome do Time")]
        public string NomeTime { get; set; } = string.Empty;

        /// <summary>
        /// Jogo/modalidade do time (League of Legends, CS:GO, Valorant, etc)
        /// </summary>
        [Required(ErrorMessage = "O jogo é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome do jogo deve ter no máximo 100 caracteres")]
        public string Jogo { get; set; } = string.Empty;

        /// <summary>
        /// Tamanho da camisa (PP, P, M, G, GG, XG)
        /// </summary>
        [Required(ErrorMessage = "O tamanho é obrigatório")]
        [StringLength(10, ErrorMessage = "O tamanho deve ter no máximo 10 caracteres")]
        public string Tamanho { get; set; } = string.Empty;

        /// <summary>
        /// Cor principal da camisa
        /// </summary>
        [StringLength(50, ErrorMessage = "A cor deve ter no máximo 50 caracteres")]
        public string? Cor { get; set; }

        /// <summary>
        /// Preço de venda da camisa
        /// </summary>
        [Required(ErrorMessage = "O preço é obrigatório")]
        [Range(0.01, 10000.00, ErrorMessage = "O preço deve estar entre R$ 0,01 e R$ 10.000,00")]
        [Display(Name = "Preço")]
        [DataType(DataType.Currency)]
        public decimal Preco { get; set; }

        /// <summary>
        /// Quantidade disponível em estoque
        /// </summary>
        [Required(ErrorMessage = "A quantidade em estoque é obrigatória")]
        [Range(0, int.MaxValue, ErrorMessage = "A quantidade deve ser maior ou igual a zero")]
        [Display(Name = "Quantidade em Estoque")]
        public int QuantidadeEstoque { get; set; }

        /// <summary>
        /// Indica se a camisa está disponível para venda
        /// </summary>
        [Display(Name = "Disponível")]
        public bool Disponivel { get; set; } = true;

        /// <summary>
        /// Data de cadastro da camisa no sistema
        /// </summary>
        [Display(Name = "Data de Cadastro")]
        [DataType(DataType.DateTime)]
        public DateTime DataCadastro { get; set; } = DateTime.Now;
    }
}