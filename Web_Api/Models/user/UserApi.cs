using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text;
using Web_Api.Models.user;

namespace Web_Api.Models.userApi
{
    public class UserApi
    {
        private static readonly string _baseUrl = Environment.GetEnvironmentVariable("USER_API") ?? "http://host.docker.internal:8080/user/";
        private static readonly HttpClient _client = new HttpClient();

        private static readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Formatting = Formatting.None // Lahko tudi nastavite na Formatting.Indented za lepši izpis, če je potrebno
        };

        public static async Task<userID[]> GetAllUserAsync()
        {
            HttpResponseMessage response = await _client.GetAsync(_baseUrl);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                userID[] borrowing = JsonConvert.DeserializeObject<userID[]>(content);
                return borrowing;
            }
            else
            {
                return null;
            }
        }

        public static async Task<userID> GetUserByID(string id)
        {
            HttpResponseMessage response = await _client.GetAsync(_baseUrl + id);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                userID borowing = JsonConvert.DeserializeObject<userID>(content);
                return borowing;
            }
            else
            {
                return null;
            }
        }

        public static async Task<bool> CreateUserAsync(user.user user)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(user, _jsonSerializerSettings), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync(_baseUrl, content);
            return response.IsSuccessStatusCode;
        }

        public static async Task<bool> UpdateUserAsync(string id, user.userID user)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(user, _jsonSerializerSettings), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PutAsync($"{_baseUrl}{id}", content);
            return response.IsSuccessStatusCode;
        }

        public static async Task<bool> DeleteUserAsync(string id)
        {
            HttpResponseMessage response = await _client.DeleteAsync($"{_baseUrl}{id}");
            return response.IsSuccessStatusCode;
        }
    }
}