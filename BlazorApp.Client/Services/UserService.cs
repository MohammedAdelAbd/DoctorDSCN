using Blazored.LocalStorage;
using SharedLibrary.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace BlazorApp.Client.Services
{
    public class UserService : IUserService
    {
         
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILocalStorageService localStorageService;

        public UserService(HttpClient httpClient, IHttpClientFactory httpClientFactory, ILocalStorageService localStorageService)
        {
             
            this._httpClientFactory = httpClientFactory;
            this.localStorageService = localStorageService;
        }


        public async Task<List<User>> GetAllUsers()
        {

            var httpClient = _httpClientFactory.CreateClient("MyApi");
            var response = await httpClient.GetAsync("api/user/users");

            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<List<User>>(responseStream, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
                throw new HttpRequestException($"Error fetching users: {response.StatusCode}");
            }
        }
        public Task<User> getUserById(string userId)
        {
            throw new NotImplementedException();
        }
        public Task AddUser(User user)
        {
            throw new NotImplementedException();
        }
        public Task UpdateUser(string userId, User user)
        {
            throw new NotImplementedException();
        }
        public Task RemoveUser(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<Response> LoginUser(User user)
        {
            //var userLogin = new StringContent(JsonSerializer.Serialize(user),
            //                                   Encoding.UTF8, "Application/json");

            //var respone = await httpClient.PostAsJsonAsync("api/user/login", userLogin);

            //var result = await respone.Content.ReadFromJsonAsync<Response>();

            //return (result!);



            var userJson = JsonSerializer.Serialize(user);

            
            var userLogin = new StringContent(userJson, Encoding.UTF8, "application/json");

            var httpClient = _httpClientFactory.CreateClient("MyApi");

            var response = await httpClient.PostAsync("api/user/login", userLogin);

             
            response.EnsureSuccessStatusCode();

             
            var result = await response.Content.ReadFromJsonAsync<Response>();

            return result;
        }
    }
}
