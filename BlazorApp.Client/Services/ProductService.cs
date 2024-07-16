using SharedLibrary.Models;
using System.Text;
using System.Text.Json;

namespace BlazorApp.Client.Services
{
    public class ProductService : IUserProductService
    {
        private readonly HttpClient httpClient;
        public ProductService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }


        public async Task<List<Product>> GetAllUsersProd()
        {
            return await JsonSerializer.DeserializeAsync<List<Product>>
                (await httpClient.GetStreamAsync("api/product"), new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                }); 
        }
        public async Task< Product> GetUserProdById(string userProdId)
        {
            return await JsonSerializer.DeserializeAsync< Product>
                (await httpClient.GetStreamAsync($"api/product/{userProdId}"), new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                });
        }
        public async Task<List< Product>> GetCustProdWithoutCustomer()
        {
            return await JsonSerializer.DeserializeAsync<List< Product>>
                (await httpClient.GetStreamAsync("api/product/userproductswithoutcustomerproducts"), new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                }); ;
        }
        public async Task<bool> AddUserProd(Product userProduct)
        {
            var userprod = new StringContent(JsonSerializer.Serialize(userProduct),
                                                Encoding.UTF8, "Application/json");
            if (userprod != null)
            {
                var respone = await httpClient.PostAsync("api/product", userprod);
                if (respone.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> UpdateUserProd(string userProdId, Product userProd)
        {
            var userprod = new StringContent(JsonSerializer.Serialize(userProd),
                                                Encoding.UTF8, "Application/json");
            if (userprod != null)
            {
                var respone = await httpClient.PutAsync($"api/product/{userProdId}", userprod);
                if (respone.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<bool>   RemoveUserProd(Guid userProdId)
        {
            if (userProdId != null)
            {
                await httpClient.DeleteAsync($"api/product/{userProdId}");
                return true;
            }
            return false;
        }
    }
}
