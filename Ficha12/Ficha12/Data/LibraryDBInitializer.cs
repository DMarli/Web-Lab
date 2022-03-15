using Ficha12.Models;

namespace Ficha12.Data
{
    static class LibraryDBInitializer
    {
        public static void InsertData(LibraryContext context)
            //Insere dados
        { 
            //Adds a publisher
            var publisher = new Publisher
            {
                Name = "Mariner Books"
            };

            context.Publishers.Add(publisher);
            //Tabela Publishers

            //Adds Books

            context.Books.Add(new Book
            //Tabela Books
            {
                ISBN = "978-353453-2222",
                Title = "The Lord of the Rings",
                Author = "J.R.R Tolkien",
                Language = "English",
                Pages = 12156,
                Publisher = publisher,
            });

            //salvar as alterações
            context.SaveChanges();
        }
    }
}
