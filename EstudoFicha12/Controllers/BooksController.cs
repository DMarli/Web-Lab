using EstudoFicha12.Models;
using EstudoFicha12.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EstudoFicha12.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : Controller
    {
        //Adicione um atributo privado do tipo IBookService
        private readonly IBookService service;

        public BooksController(IBookService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IEnumerable<Book> Get()
        {
            return service.GetAll();
        }

        // POST: BooksController/Create
        [HttpPost]
        public IActionResult Post([FromBody] Book book)
        {
            if (book != null)
            {
                Book newBook = service.Create(book);
                return CreatedAtRoute("GetByISBN", new { isbn = newBook.ISBN }, newBook);
            }

            else
            {
                return BadRequest();
            }

        }


        [HttpDelete("{isbn}")]
        // GET: BooksController/Delete/5
        public ActionResult Delete(string isbn)
        {
            var book = service.GetByISBN(isbn);

            if (book is not null)
            {
                service.DeleteByISBN(isbn);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{isbn:int}")]
        // GET: BooksController/Delete/5
        public ActionResult Get(string isbn)
        {
            var book = service.GetByISBN(isbn);

            if (book is not null)
            {
                return Ok(book);
            }
            else
            {
                return NotFound();
            }
        }


        [HttpGet("{author}")]
        // GET: BooksController/Delete/5
        public ActionResult GetByAuthor(string author)
        {
            var books = service.GetByAuthor(author);

            if (books is not null)
            {
                return Ok(books);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut("{isbn}")]
        public ActionResult Put([FromBody] Book book, string isbn)
        {
            var putBook = service.GetByISBN(isbn);

            if (putBook is not null && book is not null)
            {
                service.Update(isbn, book);
                return Ok();
            }
            else 
            {
                return NotFound();
            }
        }

        [HttpPatch("{isbn}/publisherId")]
        public IActionResult Patch(string isbn, int publisherId)
        {
            var bookToUpdate = service.GetByISBN(isbn);
            if (bookToUpdate is not null)
            {
                service.UpdatePublisher(isbn, publisherId);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
