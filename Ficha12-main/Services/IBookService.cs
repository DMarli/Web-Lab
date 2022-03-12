using Ficha12.Models;

namespace Ficha12.Services
{
    public interface IBookService
    {
        public abstract IEnumerable<Book> GetAll();
        public abstract Book Create(Book newBook);
        public abstract void DeleteByISBN(string isbn);
        public abstract Book GetByISBN(string isbn);
        public abstract void Update(string isbn, Book book);
        public abstract Book GetByAuthor(string author);
        public abstract void UpdatePublisher(string isbn, int pusblisherID);
        public abstract void Download();
    }
}
