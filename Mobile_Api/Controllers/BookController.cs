using Microsoft.AspNetCore.Mvc;
using Web_Api.Models.book;

namespace Web_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {        
        
        public BookController()
        {
                 
        }
        
        [HttpGet(Name = "books")]
        public async Task<ActionResult<IEnumerable<Book>>> Get()
        {
            var books = await BookAPI.GetAllBooksAsync();
            Console.WriteLine(books);

            if (books == null)
            {
                return NotFound();
            }
            return Ok(books);
        }
        
        [HttpGet("{id}", Name = "getBookById")]
        public async Task<ActionResult<Book>> GetById(string id)
        {
            var book = await BookAPI.GetBookByID(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }


        
     

        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Book book)
        {
            var success = await BookAPI.CreateBookAsync(book);
            if (success)
            {
                return CreatedAtRoute("books", null, book);
            }
            return BadRequest();
        }
        
        
    }
}
