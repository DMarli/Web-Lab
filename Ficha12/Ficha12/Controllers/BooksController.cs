using Ficha12.Models;
using Ficha12.Services;
using Microsoft.AspNetCore.Mvc;
using Ficha12.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ficha12.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    //disponibiliza endpoints para fazermos ações sobre o recurso livro
    {

            private readonly IBookService service;

            public BooksController(IBookService service)
            {
                this.service = service;
            }
            //intermediário entre o Controller e o DBContext
            //podia fazer no Controller mas não é boa prática

            [HttpGet]

            public IEnumerable<Book> Get()
            {
                return service.GetAll();
                //retorna método do serviço
            }

            [HttpGet("{isbn}", Name = "GetByISBN")]

            //Não obrigatório
            [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Book))]
            [ProducesResponseType(StatusCodes.Status404NotFound)]

            public IActionResult Get(string isbn)
                {
                    Book? oneBook = service.GetByISBN(isbn);

                    if (oneBook != null)
                    {
                        return Ok(oneBook);
                    }
                    else
                    {
                        return NotFound();
                    }
                }

            // POST api/<ValuesController>
            [HttpPost]
            public Book Create(Book newBook)
            {
                return service.Create(newBook);
            }

            [HttpDelete]
            public Book DeleteByISBN(Book newBook)
            {
             service.DeleteByISBN(newBook.ISBN);
            }
    }
}

