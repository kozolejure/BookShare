using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Web_Api.Models.borrowing;

namespace Web_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [TokenValidationFilter("skrivnost")]
    public class BorrowController : Controller
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BorrowingId>>> Get()
        {
            var books = await BorrowingApi.GetAllBorrowingAsync();
            if (books == null)
            {
                return NotFound();
            }
            Stats("Get borrowing");
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
            Stats("Get borrowing");

            return Ok(book);
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var success = await BorrowingApi.DeleteBorrowingAsync(id);
            if (success)
            {
                Stats("Get borrowing by id");

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
                Stats("add borrowing");

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
            Stats("delete borrowing");
            return Ok(boksWhituserId);

        }


        [HttpPut("{id}")]
        public async Task<IActionResult> GetAllboworingsOfUser(string id, [FromBody] BorrowingId book)
        {
            var success = await BorrowingApi.UpdateBorrowingAsync(id, book);
            if (success)
            {
                Stats("edit borrowing");

                return NoContent();
            }
            return NotFound();
        }
    }
}
