using SharedLibrary.Models;

namespace BlazorApp.Client.Services
{
    public interface ICustomerProductService
    { 
            Task<List<CustomerProduct>> GetAllCustProd();
            Task<CustomerProduct> getCustProdById(string userCustId);
            Task<bool> AddCustProduct(CustomerProduct userCustomer);
            Task<bool> UpdateCustomerProduct(string userCustId, CustomerProduct userCustomer);
            Task<bool> RemoveCustomerProduct(Guid userId);
    }
}
