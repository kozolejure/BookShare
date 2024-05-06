using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Web_Api.Models.book;

namespace Web_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [TokenValidationFilter("skrivnost")]
    public class BookController : ControllerBase
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
            Stats("get Books");
            return Ok(books);
        }
        
        [HttpGet("{id}", Name = "getBookById")]
        public async Task<ActionResult<Book>> GetById(string id)
        {
            var book = await BookAPI.GetBookByID(id);
            if (book == null)
            {
               
                return NotFound();
                //test
            }
            Stats("get Book by id");
            return Ok(book);
        }


        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var success = await BookAPI.DeleteBookAsync(id);
            if (success)
            {
                return NoContent();
            }
            Stats("delete Book");
            return NotFound();
        }

        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Book book)
        {
            var success = await BookAPI.CreateBookAsync(book);
            if (success)
            {
                Stats("add book");
                return CreatedAtRoute("books", null, book);
            }
            
            return BadRequest();
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] Book book)
        {
            var success = await BookAPI.UpdateBookAsync(id, book);
            if (success)
            {
                Stats("edit Book");
                return NoContent();
            }
            return NotFound();
        }
    }
}
