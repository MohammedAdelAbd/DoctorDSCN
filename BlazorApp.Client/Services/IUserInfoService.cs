using SharedLibrary.Models;

namespace BlazorApp.Client.Services
{
    public interface IUserInfoService
    {
        Task<IEnumerable<UserInfo>> GetAllUsersInfos();
        Task<UserInfo> getUserInfoById(string userInfoId);
        Task<bool> AddUserInfo(UserInfo userInfo);


        Task<Response> RegisterUser(UserInfo userInfo);
         
    }
}
