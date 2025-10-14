using BibliotecaApp.Domain.Entities;

namespace BibliotecaApp.Infrastructure.Factories
{
    public static class LivroFactory
    {
        public static Livro CriarLivro(string titulo, string autor, string isbn, int anoPublicacao)
        {
            return new Livro(titulo, autor, isbn, anoPublicacao);
        }

        public static Livro CriarLivroClassico(string titulo, string autor, string isbn)
        {
            // Livros clássicos antes de 1900
            return new Livro(titulo, autor, isbn, 1850);
        }

        public static Livro CriarLivroModerno(string titulo, string autor, string isbn)
        {
            // Livros modernos do ano atual
            return new Livro(titulo, autor, isbn, System.DateTime.Now.Year);
        }
    }
}
