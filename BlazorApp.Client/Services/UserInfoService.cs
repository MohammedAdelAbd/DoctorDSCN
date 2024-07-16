using SharedLibrary.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace BlazorApp.Client.Services
{
    public class UserInfoService : IUserInfoService
    {
        //private readonly HttpClient httpClient;
        private readonly IHttpClientFactory _httpClientFactory;

        public UserInfoService(HttpClient httpClient,IHttpClientFactory  httpClientFactory)
        {
            //this.httpClient = httpClient;
            this._httpClientFactory = httpClientFactory;
        }
        public Task<IEnumerable<UserInfo>> GetAllUsersInfos()
        {
            throw new NotImplementedException();
        }
        public Task<UserInfo> getUserInfoById(string userInfoId)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> AddUserInfo(UserInfo userInfo)
        {

             
            var userinfo = new StringContent(JsonSerializer.Serialize(userInfo), 
                                                Encoding.UTF8, "Application/json");
            if (userInfo != null)
            {
                var httpClient = _httpClientFactory.CreateClient("MyApi");
                var respone =  await httpClient.PostAsync("api/userinfo/register", userinfo);
                if (respone.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<Response> RegisterUser(UserInfo userInfo)
        {

            var userInfoJson = JsonSerializer.Serialize(userInfo);


            var userInfoLogin = new StringContent(userInfoJson, Encoding.UTF8, "application/json");

            var httpClient = _httpClientFactory.CreateClient("MyApi");

            var response = await httpClient.PostAsync("api/userinfo/register", userInfoLogin);



            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<Response>();

            return (result!);
        }
    }
}
