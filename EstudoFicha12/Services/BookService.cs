using EstudoFicha12.Models;
using Microsoft.EntityFrameworkCore;

namespace EstudoFicha12.Services
{
    public class BookService : IBookService
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

        public Book Create(Book newBook)
        {
            //Publisher pub = context.Publishers.Find(newBook.Publisher.ID);

            //if (pub is null)
            //{
            //    throw new NullReferenceException("Publisher does not exist");
            //}
            //else
            //{
            //    newBook.Publisher = pub;
            //    context.Books.Add(newBook);
            //    context.SaveChanges();
            //    return newBook;
            //}
        }

        public void DeleteByISBN(string isbn)
        {
            throw new NotImplementedException();
        }

        public Book? GetByISBN(string isbn)
        {
            throw new NotImplementedException();
        }

        public Book Update(string isbn, Book book)
        {
            throw new NotImplementedException();
        }

        public void UpdatePublisher(string isbn, int publisherId)
        {
            throw new NotImplementedException();
        }
    }
}
