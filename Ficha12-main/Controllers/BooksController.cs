using Ficha12.Models;
using Ficha12.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ficha12.Controllers
{
    public class BooksController : ControllerBase
    {
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

        //[HttpGet("{isbn:int}", Name = "GetByISBN")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Book))]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]

        //public IActionResult Get(int isbn)
        //{
        //    Book? book = book.Find(e => e.UserId == id);
            
        //    if (e != null)
        //    {
        //        return Ok(e);
        //    }
        //    else
        //    {
        //        return NotFound($"ID: {isbn} Not Found");
        //    }
        //}

    }
}
