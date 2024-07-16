using SharedLibrary.Models;

namespace BlazorApp.Client.Services
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsers();
        Task<User> getUserById(string userId);
        Task  AddUser(User user);
        Task  UpdateUser(string userId, User user);
        Task  RemoveUser(string userId);


        Task<Response> LoginUser(User user);
    }
}
