using Ficha12.Models;

namespace Ficha12.Data
{
    static class LibraryDBInitializer
    {
        public static void InsertData(LibraryContext context)
        { 
            //Adds a publisher
            var publisher = new Publisher
            {
                Name = "Mariner Books"
            };

            context.Publishers.Add(publisher);

            //Adds Books

            context.Books.Add(new Book
            {
                ISBN = "978-353453-2222",
                Title = "The Lord of the Rings",
                Author = "J.R.R Tolkien",
                Language = "Emglish",
                Pages = 12156,
                Publisher = publisher,
            });
        }
    }
}
