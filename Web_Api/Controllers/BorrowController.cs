using Microsoft.AspNetCore.Mvc;
using Web_Api.Models.borrowing;

namespace Web_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BorrowController : Controller
    {

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BorrowingId>>> Get()
        {
            var books = await BorrowingApi.GetAllBorrowingAsync();
            if (books == null)
            {
                return NotFound();
            }
            return Ok(books);
        }

        [HttpGet("{id}", Name = "getBorrowngById")]
        public async Task<ActionResult<BorrowingId>> GetById(string id)
        {
            var book = await BorrowingApi.GetBorrowingByID(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var success = await BorrowingApi.DeleteBorrowingAsync(id);
            if (success)
            {
                return NoContent();
            }
            return NotFound();
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Borrowing Borrowing)
        {
            var success = await BorrowingApi.CreateBorrowingAsync(Borrowing);
            if (success)
            {
                return CreatedAtRoute("books", null, Borrowing);
            }
            return BadRequest();
        }

        [HttpGet("/Borrow/ofuser/{id}")]
        public async Task<IActionResult> Update(string id)
        {
            var books = await BorrowingApi.GetAllBorrowingAsync();
            var boksWhituserId = books.Where(x => x.UserId == id);
            if (boksWhituserId == null)
            {
                return NotFound();
            }
            return Ok(boksWhituserId);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> GetAllboworingsOfUser(string id, [FromBody] BorrowingId book)
        {
            var success = await BorrowingApi.UpdateBorrowingAsync(id, book);
            if (success)
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}
