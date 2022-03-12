using Ficha12.Models;
using Microsoft.EntityFrameworkCore;

namespace Ficha12.Services
{
    public class BookService: IBookService
    {
        private readonly LibraryContext context;

        public BookService(LibraryContext context)
        {
            this.context = context;
        }

        public IEnumerable<Book> GetAll()
        {
            var books = context.Books
            .Include(p => p.Publisher);
            return books;
        }

        public Book GetByAuthor(string author)
        {
            var book = context.Books.Include(b => b.Publisher).SingleOrDefault(b => b.Author == author);
            return book;
        }

        public Book GetByISBN(string isbn)
        {
            var book = context.Books.Include(b => b.Publisher).SingleOrDefault(b => b.ISBN == isbn);
            return book;
        }

        //errado
        public Book Create(Book newBook)
        {
            throw new NotImplementedException();
        }

        //errado
        public void DeleteByISBN(string isbn)
        {
            var book = context.Books.Include(b => b.Publisher).SingleOrDefault(b => b.ISBN == isbn);
            context.Books.Remove(book);
        }

        public void Download()
        {
            
        }

        public void Update(string isbn, Book book)
        {
            
        }

        public void UpdatePublisher(string isbn, int pusblisherID)
        {
            
        }
    }

}
