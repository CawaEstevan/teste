using System;
using System.ComponentModel.DataAnnotations;

namespace BibliotecaApp.Domain.Entities
{
    public class Livro
    {
        public int Id { get; private set; }
        
        [Required(ErrorMessage = "O título é obrigatório")]
        [StringLength(200, ErrorMessage = "O título deve ter no máximo 200 caracteres")]
        public string Titulo { get; private set; }
        
        [Required(ErrorMessage = "O autor é obrigatório")]
        [StringLength(150, ErrorMessage = "O autor deve ter no máximo 150 caracteres")]
        public string Autor { get; private set; }
        
        [Required(ErrorMessage = "O ISBN é obrigatório")]
        [RegularExpression(@"^\d{13}$", ErrorMessage = "O ISBN deve conter 13 dígitos")]
        public string ISBN { get; private set; }
        
        [Range(1450, 2100, ErrorMessage = "Ano inválido")]
        public int AnoPublicacao { get; private set; }
        
        public bool Disponivel { get; private set; }
        
        public DateTime DataCriacao { get; private set; }

        // Construtor privado para EF
        private Livro() { }

        // Construtor para criação de novos livros
        public Livro(string titulo, string autor, string isbn, int anoPublicacao)
        {
            ValidarDados(titulo, autor, isbn, anoPublicacao);
            
            Titulo = titulo;
            Autor = autor;
            ISBN = isbn;
            AnoPublicacao = anoPublicacao;
            Disponivel = true;
            DataCriacao = DateTime.Now;
        }

        // Métodos de negócio
        public void Atualizar(string titulo, string autor, string isbn, int anoPublicacao)
        {
            ValidarDados(titulo, autor, isbn, anoPublicacao);
            
            Titulo = titulo;
            Autor = autor;
            ISBN = isbn;
            AnoPublicacao = anoPublicacao;
        }

        public void MarcarComoDisponivel() => Disponivel = true;
        
        public void MarcarComoIndisponivel() => Disponivel = false;

        private void ValidarDados(string titulo, string autor, string isbn, int anoPublicacao)
        {
            if (string.IsNullOrWhiteSpace(titulo))
                throw new ArgumentException("Título não pode ser vazio");
            
            if (string.IsNullOrWhiteSpace(autor))
                throw new ArgumentException("Autor não pode ser vazio");
            
            if (string.IsNullOrWhiteSpace(isbn) || isbn.Length != 13)
                throw new ArgumentException("ISBN deve ter 13 dígitos");
            
            if (anoPublicacao < 1450 || anoPublicacao > DateTime.Now.Year)
                throw new ArgumentException("Ano de publicação inválido");
        }
    }
}
