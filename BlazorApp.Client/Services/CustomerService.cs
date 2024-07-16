using SharedLibrary.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace BlazorApp.Client.Services
{
    public class CustomerService : ICustomerService
    {
         
        private readonly IHttpClientFactory _httpClientFactory;

        public CustomerService(HttpClient httpClient , IHttpClientFactory httpClientFactory)
        {
             
            this._httpClientFactory = httpClientFactory;
        }
        public async Task<List<Customer>> GetAllUsersCust()
        {
            var httpClient = _httpClientFactory.CreateClient("MyApi");

            return await JsonSerializer.DeserializeAsync<List<Customer>>
                (await httpClient.GetStreamAsync("api/usercustomer"), new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                }); ;
        }
        public async Task<Customer> getUserCustById(string userCustId)
        {
            var httpClient = _httpClientFactory.CreateClient("MyApi");

            return await JsonSerializer.DeserializeAsync<Customer>
                (await httpClient.GetStreamAsync($"api/usercustomer/{userCustId}"), new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                });
        }
        public async Task<bool> AddUserCust(Customer userCustomer)
        {
            var uCustomer = new StringContent(JsonSerializer.Serialize(userCustomer),
                                                Encoding.UTF8, "Application/json");
            if (userCustomer != null)
            {
                var httpClient = _httpClientFactory.CreateClient("MyApi");

                var respone = await httpClient.PostAsync("api/usercustomer", uCustomer);
                if (respone.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> UpdateUserCustomer(string userCustId, Customer userCustomer)
        {
            var userCust = new StringContent(JsonSerializer.Serialize(userCustomer),
                                                Encoding.UTF8, "Application/json");
            var httpClient = _httpClientFactory.CreateClient("MyApi");

            if (userCust != null)
            {


                var respone = await httpClient.PutAsync($"api/usercustomer/{userCustId}", userCust);
                if (respone.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> RemoveUserCustomer(Guid userId)
        {
            if (userId != null)
            {
                var httpClient = _httpClientFactory.CreateClient("MyApi");

                await httpClient.DeleteAsync($"api/usercustomer/{userId}");
                return true;
            }
            return false;
        }
    }
}
