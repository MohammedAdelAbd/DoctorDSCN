using SharedLibrary.Models;
using System.Text;
using System.Text.Json;

namespace BlazorApp.Client.Services
{
    public class CustomerProductService : ICustomerProductService
    {
        private readonly HttpClient httpClient;

        public CustomerProductService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<List<CustomerProduct>> GetAllCustProd()
        {
            return await JsonSerializer.DeserializeAsync<List<CustomerProduct>>
                (await httpClient.GetStreamAsync("api/customerproduct"), new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                }); ;
        }
        
        public Task<CustomerProduct> getCustProdById(string userCustId)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> AddCustProduct(CustomerProduct CustomerProduct)
        {
            var custprod = new StringContent(JsonSerializer.Serialize(CustomerProduct),
                                                Encoding.UTF8, "Application/json");
            if (custprod != null)
            {
                var respone = await httpClient.PostAsync("api/customerproduct", custprod);
                if (respone.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            return false;
        }
        public Task<bool> RemoveCustomerProduct(Guid userId)
        {
            throw new NotImplementedException();
        }
        public Task<bool> UpdateCustomerProduct(string userCustId, CustomerProduct userCustomer)
        {
            throw new NotImplementedException();
        }
    }
}

