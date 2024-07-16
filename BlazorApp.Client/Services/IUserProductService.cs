using SharedLibrary.Models;

namespace BlazorApp.Client.Services
{
    public interface IUserProductService
    {
        Task<List<Product>> GetAllUsersProd();
        Task< Product> GetUserProdById(string userProdId);
        Task<List< Product>> GetCustProdWithoutCustomer();

        Task<bool> AddUserProd(Product userProduct);
        Task<bool> UpdateUserProd(string userProdId, Product userProd);
        Task<bool> RemoveUserProd(Guid userId);

    }
}
