using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text;

namespace Web_Api.Models.borrowing
{
    public class BorrowingApi
    {
        private static readonly string _baseUrl = Environment.GetEnvironmentVariable("BORROWING_API") ?? "http://host.docker.internal:8089/borrowings/";
        private static readonly HttpClient _client = new HttpClient();

        private static readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Formatting = Formatting.None // Lahko tudi nastavite na Formatting.Indented za lepši izpis, če je potrebno
        };

        public static async Task<BorrowingId[]> GetAllBorrowingAsync()
        {
            HttpResponseMessage response = await _client.GetAsync(_baseUrl);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                BorrowingId[] borrowing = JsonConvert.DeserializeObject<BorrowingId[]>(content);
                return borrowing;
            }
            else
            {
                return null;
            }
        }


        public static async Task<BorrowingId> GetBorrowingByID(string id)
        {
            HttpResponseMessage response = await _client.GetAsync(_baseUrl + "/" + id);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                BorrowingId borowing = JsonConvert.DeserializeObject<BorrowingId>(content);
                return borowing;
            }
            else
            {
                return null;
            }
        }

        public static async Task<bool> CreateBorrowingAsync(Borrowing borrowing)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(borrowing, _jsonSerializerSettings), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync(_baseUrl, content);
            return response.IsSuccessStatusCode;
        }

        public static async Task<bool> UpdateBorrowingAsync(string id, BorrowingId book)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(book, _jsonSerializerSettings), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PutAsync($"{_baseUrl}/{id}", content);
            return response.IsSuccessStatusCode;
        }

        public static async Task<bool> DeleteBorrowingAsync(string id)
        {
            HttpResponseMessage response = await _client.DeleteAsync($"{_baseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}