using SharedLibrary.Models;

namespace BlazorApp.Client.Services
{
    public interface ICustomerService
    { 
            Task<List<Customer>> GetAllUsersCust();
            Task<Customer> getUserCustById(string userCustId);
            Task<bool> AddUserCust(Customer userCustomer);
            Task<bool> UpdateUserCustomer(string userCustId, Customer userCustomer);
            Task<bool> RemoveUserCustomer(Guid userId);
    }
}
