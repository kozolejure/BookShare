using Microsoft.AspNetCore.Mvc;
using Web_Api.Models.book;
using Web_Api.Models.borrowing;
using Web_Api.Models.user;
using Web_Api.Models.userApi;

namespace Web_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        [HttpGet(Name = "Users")]
        public async Task<ActionResult<IEnumerable<userID>>> Get()
        {
            var books = await UserApi.GetAllUserAsync();
            if (books == null)
            {
                return NotFound();
            }
            return Ok(books);
        }

        [HttpGet("{id}", Name = "getUserById")]
        public async Task<ActionResult<userID>> GetById(string id)
        {
            var book = await UserApi.GetUserByID(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var success = await UserApi.DeleteUserAsync(id);
            if (success)
            {
                return NoContent();
            }
            return NotFound();
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] user user)
        {
            var success = await UserApi.CreateUserAsync(user);
            if (success)
            {
                return CreatedAtRoute("users", null, user);
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] Book book)
        {
            var success = await BookAPI.UpdateBookAsync(id, book);
            if (success)
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}
