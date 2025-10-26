using System.ComponentModel.DataAnnotations;

namespace LojaCamisasGames.Application.DTOs
{
    /// <summary>
    /// DTO para criação de novas camisas
    /// </summary>
    public class CamisaGameCreateDto
    {
        [Required(ErrorMessage = "O nome da camisa é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres")]
        [Display(Name = "Nome da Camisa")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O nome do time é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome do time deve ter no máximo 100 caracteres")]
        [Display(Name = "Nome do Time")]
        public string NomeTime { get; set; } = string.Empty;

        [Required(ErrorMessage = "O jogo é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome do jogo deve ter no máximo 100 caracteres")]
        [Display(Name = "Jogo")]
        public string Jogo { get; set; } = string.Empty;

        [Required(ErrorMessage = "O tamanho é obrigatório")]
        [StringLength(10, ErrorMessage = "O tamanho deve ter no máximo 10 caracteres")]
        [Display(Name = "Tamanho")]
        public string Tamanho { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "A cor deve ter no máximo 50 caracteres")]
        [Display(Name = "Cor")]
        public string? Cor { get; set; }

        [Required(ErrorMessage = "O preço é obrigatório")]
        [Range(0.01, 10000.00, ErrorMessage = "O preço deve estar entre R$ 0,01 e R$ 10.000,00")]
        [Display(Name = "Preço (R$)")]
        [DataType(DataType.Currency)]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "A quantidade em estoque é obrigatória")]
        [Range(0, int.MaxValue, ErrorMessage = "A quantidade deve ser maior ou igual a zero")]
        [Display(Name = "Quantidade em Estoque")]
        public int QuantidadeEstoque { get; set; }

        [Display(Name = "Disponível para Venda")]
        public bool Disponivel { get; set; } = true;
    }
}