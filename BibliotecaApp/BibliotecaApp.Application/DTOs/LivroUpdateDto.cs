using System.ComponentModel.DataAnnotations;

namespace BibliotecaApp.Application.DTOs
{
    public class LivroUpdateDto
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "O título é obrigatório")]
        [StringLength(200)]
        public string Titulo { get; set; }
        
        [Required(ErrorMessage = "O autor é obrigatório")]
        [StringLength(150)]
        public string Autor { get; set; }
        
        [Required(ErrorMessage = "O ISBN é obrigatório")]
        [RegularExpression(@"^\d{13}$", ErrorMessage = "O ISBN deve conter 13 dígitos")]
        public string ISBN { get; set; }
        
        [Required(ErrorMessage = "O ano de publicação é obrigatório")]
        [Range(1450, 2100)]
        public int AnoPublicacao { get; set; }
        
        public bool Disponivel { get; set; }
    }
}
