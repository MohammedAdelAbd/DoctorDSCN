using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.Models;
using System;
using System.Security.Cryptography;
using WebApplication2.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController ]
    public class UserInfoController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public UserInfoController(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }
        // GET: api/<UserInfoController>
        [HttpGet]
        public async Task<List<UserInfo>> GetAllUserInfo()
        {
            return  _appDbContext.userInfos.ToList();
        }

        // GET api/<UserInfoController>/5
        [HttpGet("{id}")]
        public async Task<UserInfo> GetInfoById(string id)
        {
            var userResult = _appDbContext.userInfos.Where(x => x.UserInfoId == Guid.Parse(id)).FirstOrDefault();

            return userResult;
        }

        // POST api/<UserInfoController> 
        [HttpPost("register")]
        public async Task<Response> UserRegisstration(UserInfo userInfo)
        {
            var user = _appDbContext.Users.Where(x => x.UserName == userInfo.User.UserName).FirstOrDefault();

            if (user != null)
            {
                return (new Response() { seccess = false, message = "User already Exist!" });
            }
            if (user == null)
            {
                user = new User
                {
                    UserID = userInfo.User.UserID,
                    UserName = userInfo.User.UserName,
                    UserPassword = HashPassword(userInfo.User.UserPassword)
                };
                _appDbContext.Users.Add(user);
                _appDbContext.SaveChanges();
            }

            // Set the existing user to the userInfo
            userInfo.UserID = user.UserID;
            userInfo.User = user;



            // start with add user role
            var recentlyUserAdded =  _appDbContext.Users.Where(_ => _.UserName == userInfo.User.UserName).FirstOrDefault();
            var userRole = new UserRole();
            if (userInfo.User.UserName!.StartsWith("admin"))
            {
                userRole.UserId = recentlyUserAdded!.UserID;
                userRole.Role = "Admin";
            }
            else
            {
                userRole.UserId = recentlyUserAdded!.UserID;
                userRole.Role = "User";
            }
            //end with add role


            _appDbContext.UserRoles.Add(userRole);
            _appDbContext.SaveChanges();


            _appDbContext.userInfos.Add(userInfo);
            _appDbContext.SaveChanges();
            return (new Response() { seccess = true, message = "Account Seccessfully Created!" });
             
        }

        // PUT api/<UserInfoController>/5
        [HttpPut("{id}")]
        public void Put(string id, UserInfo userInfo)
        {
            UserInfo? editInfoUser = _appDbContext.userInfos.FirstOrDefault(x => x.UserInfoId == Guid.Parse(id));

            editInfoUser.InfoName = userInfo.InfoName;
            editInfoUser.InfoEmail = userInfo.InfoEmail;
            editInfoUser.InfoAdress = userInfo.InfoAdress;
            editInfoUser.InfoImage = userInfo.InfoImage;
            editInfoUser.InfoNameEnglish = userInfo.InfoNameEnglish;
            editInfoUser.InfoPhone = userInfo.InfoPhone;


            _appDbContext.SaveChanges();
        }

        // DELETE api/<UserInfoController>/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            var userInfoResult = _appDbContext.userInfos.Where(x => x.UserInfoId == Guid.Parse(id)).FirstOrDefault();
            _appDbContext.userInfos.Remove(userInfoResult);
            _appDbContext.SaveChangesAsync();
        }

        //Hashing Your Password
        private string HashPassword(string plainPassword)
        {
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            var rfcPassord = new Rfc2898DeriveBytes(plainPassword, salt, 1000, HashAlgorithmName.SHA1);
            byte[] rfcPasswordHash = rfcPassord.GetBytes(20);

            byte[] passwordHash = new byte[36];
            Array.Copy(salt, 0, passwordHash, 0, 16);
            Array.Copy(rfcPasswordHash, 0, passwordHash, 16, 20);


            return Convert.ToBase64String(passwordHash);
        }

    }
}
