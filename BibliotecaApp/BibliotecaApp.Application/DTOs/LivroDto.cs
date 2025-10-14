using System;

namespace BibliotecaApp.Application.DTOs
{
    public class LivroDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string ISBN { get; set; }
        public int AnoPublicacao { get; set; }
        public bool Disponivel { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
