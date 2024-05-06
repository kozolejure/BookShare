using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Web_Api.Models.book;
using Web_Api.Models.borrowing;
using Web_Api.Models.user;
using Web_Api.Models.userApi;

namespace Web_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [TokenValidationFilter("skrivnost")]
    public class UserController : Controller
    {

        static async Task Stats(string inputString)
        {
            string apiUrl = "http://stats:3000/stats"; // URL vaše storitve

            // Podatki, ki jih želimo poslati v telesu zahtevka
            string jsonString = "{\"string\": \"" + inputString + "\"}";

            // Ustvarimo novo instanco HttpClient
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Pošljemo POST zahtevek z uporabo vsebine JSON
                    HttpResponseMessage response = await client.PostAsync(apiUrl, new StringContent(jsonString, Encoding.UTF8, "application/json"));

                    // Preverimo, ali je bil odgovor uspešen (status koda 2xx)
                    if (response.IsSuccessStatusCode)
                    {
                        // Izpišemo odgovor strežnika
                        Console.WriteLine("Odgovor strežnika: " + await response.Content.ReadAsStringAsync());
                    }
                    else
                    {
                        Console.WriteLine("Napaka: " + response.StatusCode);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Napaka pri pošiljanju zahtevka: " + ex.Message);
                }
            }
        }


            [HttpGet(Name = "Users")]
        public async Task<ActionResult<IEnumerable<userID>>> Get()
        {
            var books = await UserApi.GetAllUserAsync();
            if (books == null)
            {
                return NotFound();
            }
            await Stats("Get user stats");
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

            await Stats("Get user by id");
            return Ok(book);
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var success = await UserApi.DeleteUserAsync(id);
            if (success)
            {
                await Stats("Delete user");
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
                await Stats("Create user");

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
                await Stats("Edit user");
                return NoContent();
            }
            return NotFound();
        }
    }
}
