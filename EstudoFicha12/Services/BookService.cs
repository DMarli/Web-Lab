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
            //Buscar o Books da base de dados e incluir Publisher

            var books = context.Books
            .Include(p => p.Publisher);
            return books;
        }

        public Book Create(Book newBook)
        {
            Publisher pub = context.Publishers.Find(newBook.Publisher.ID);

            if (pub is null)
            {
                throw new NullReferenceException("Publisher doesn't exist");
            }

            else
            { 
                newBook.Publisher = pub;
                context.Books.Add(newBook);
                context.SaveChanges();
                return newBook;
            }

        }

        public void DeleteByISBN(string isbn)
        {
            var deleteBook = context.Books.Find(isbn);

            if (deleteBook != null)
            {
                context.Books.Remove(deleteBook);
                context.SaveChanges();  
            }
            
        }

        public Book? GetByISBN(string isbn)
        {
            var book = context.Books
                .Include(p => p.Publisher)
                .SingleOrDefault(b => b.ISBN == isbn);
            return book;
        }

        public void Update(string isbn, Book book)
        {
            var bookToUpdate = context.Books.Find(isbn);
            if (bookToUpdate is null)
            {
                throw new NullReferenceException("Book does not exist");
            }
            else
            {
                Publisher pub = context.Publishers.Find(book.Publisher.ID);
                bookToUpdate.Title = book.Title;
                bookToUpdate.Pages = book.Pages;
                bookToUpdate.Publisher = pub;
                bookToUpdate.Language = book.Language;
                bookToUpdate.Author = book.Author;

                context.SaveChanges();
            }
        }

        public void UpdatePublisher(string isbn, int publisherId)
        {
            var bookToUpdate = context.Books.Find(isbn);
            var publisherToUpdate = context.Publishers.Find(publisherId);

            if (bookToUpdate is null || publisherToUpdate is null)
            {
                throw new NullReferenceException("Book or publisher does not exist");
            }

            bookToUpdate.Publisher = publisherToUpdate;

            context.SaveChanges();
        }

        public IEnumerable<Book> GetByAuthor(string author)
        {
            var books = context.Books
                .Where(p => p.Author == author);
                
            return books;
        }
    }
}
