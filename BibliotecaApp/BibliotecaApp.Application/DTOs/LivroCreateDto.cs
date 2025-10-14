using System.ComponentModel.DataAnnotations;

namespace BibliotecaApp.Application.DTOs
{
    public class LivroCreateDto
    {
        [Required(ErrorMessage = "O título é obrigatório")]
        [StringLength(200, ErrorMessage = "O título deve ter no máximo 200 caracteres")]
        public string Titulo { get; set; }
        
        [Required(ErrorMessage = "O autor é obrigatório")]
        [StringLength(150, ErrorMessage = "O autor deve ter no máximo 150 caracteres")]
        public string Autor { get; set; }
        
        [Required(ErrorMessage = "O ISBN é obrigatório")]
        [RegularExpression(@"^\d{13}$", ErrorMessage = "O ISBN deve conter 13 dígitos")]
        public string ISBN { get; set; }
        
        [Required(ErrorMessage = "O ano de publicação é obrigatório")]
        [Range(1450, 2100, ErrorMessage = "Ano deve estar entre 1450 e 2100")]
        public int AnoPublicacao { get; set; }
    }
}
