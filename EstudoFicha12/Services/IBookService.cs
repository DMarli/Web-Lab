using EstudoFicha12.Models;

namespace EstudoFicha12.Services
{
    public interface IBookService
    {
        public abstract IEnumerable<Book> GetAll(); 
        //Recebe lista de todos os livros, não recebe parâmetros
        public abstract Book? GetByISBN(string isbn); 
        //1 parâmetro - Livro obtido através do ISBN, o ? é porque é required
        public abstract Book Create(Book newBook); 
        //1 parâmetro - Criar recebe 1 novo livro
        public abstract void DeleteByISBN(string isbn); 
        //1 parâmetro - Não retorna nada, só apaga
        public abstract void Update(string isbn, Book book);
        //2 parâmetros - Através do ISBN vai atualizar o livro correspondente
        public abstract void UpdatePublisher(string isbn, int publisherId);
        //2 parâmetros - Precisa do ISBN e o ID do publisher para fazer o Update

        // public abstract void Download();
        public IEnumerable<Book> GetByAuthor(string author);
        //Para lista de livros do mesmo author
    }
}












