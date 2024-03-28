using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Web_Api.Models.book
{
    public class BookAPI
    {
        private static readonly string _baseUrl = Environment.GetEnvironmentVariable("BOOK_API") ?? "http://host.docker.internal:8088/book";
        private static readonly HttpClient _client = new HttpClient();
        private static readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Formatting = Formatting.None // Lahko tudi nastavite na Formatting.Indented za lepši izpis, če je potrebno
        };

        public static async Task<BookID> GetBookAsync(int id)
        {
            HttpResponseMessage response = await _client.GetAsync($"{_baseUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                BookID book = JsonConvert.DeserializeObject<BookID>(content);
                return book;
            }
            else
            {
                // Handle failure case or throw exception
                return null;
            }
        }

        public static async Task<BookID[]> GetAllBooksAsync()
        {

            Console.WriteLine("izvedem se");
            HttpResponseMessage response = await _client.GetAsync(_baseUrl);
            Console.WriteLine(_baseUrl);

            if (response.IsSuccessStatusCode)
            {

                string content = await response.Content.ReadAsStringAsync();
                BookID[] books = JsonConvert.DeserializeObject<BookID[]>(content);
                return books;
            }
            else
            {
                Console.WriteLine($"Status Code: {response.StatusCode}");
                Console.WriteLine($"Reason Phrase: {response.ReasonPhrase}");

                // Poskusite prebrati vsebino odgovora za več informacij
                string responseContent = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrWhiteSpace(responseContent))
                {
                    Console.WriteLine("Response Content:");
                    Console.WriteLine(responseContent);
                }

                // Dodatno, lahko izpišete tudi glave odgovora za več konteksta
                Console.WriteLine("Response Headers:");
                foreach (var header in response.Headers)
                {
                    Console.WriteLine($"{header.Key}: {string.Join(", ", header.Value)}");
                }

                return null;
            }
        }


        public static async Task<Book> GetBookByID(string id)
        {
            HttpResponseMessage response = await _client.GetAsync(_baseUrl + "/" + id);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                Book books = JsonConvert.DeserializeObject<Book>(content);
                return books;
            }
            else
            {
                return null;
            }
        }

        public static async Task<bool> CreateBookAsync(Book book)
        {

            
            StringContent content = new StringContent(JsonConvert.SerializeObject(book, _jsonSerializerSettings), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync(_baseUrl, content);
            
            return response.IsSuccessStatusCode;
        }

        public static async Task<bool> UpdateBookAsync(string id, Book book)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(book, _jsonSerializerSettings), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PutAsync($"{_baseUrl}/{id}", content);
            return response.IsSuccessStatusCode;
        }

        public static async Task<bool> DeleteBookAsync(string id)
        {
            HttpResponseMessage response = await _client.DeleteAsync($"{_baseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}