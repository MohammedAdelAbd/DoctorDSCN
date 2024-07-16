using Azure.Messaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SharedLibrary.Models;
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebApplication2.Data;
 


namespace WebApplication2.Controllers
{
     
    [Route("api/[controller]")]
    [ApiController]
     
    public class UserController : ControllerBase
    {
        private IConfiguration _config;
        private readonly AppDbContext _appDbContext;
        public UserController(IConfiguration config,AppDbContext appDbContext)
        {
                this._appDbContext = appDbContext;
                this._config = config;
        }
        // GET: api/<UserController>
        [HttpGet("users")]
         
        public async Task<IEnumerable<User>> GetAllUser()
        {
             
            return await _appDbContext.Users.ToListAsync();
        }
        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<User> GetUserById(string id)
        {
            var userResult = _appDbContext.Users.Where(x=>x.UserID == Guid.Parse(id)).FirstOrDefault();
             
            return userResult;
        }
        // POST api/<UserController>
        [HttpPost]
        public void Post(User user)
        { 
                _appDbContext.Users.Add(user);
                _appDbContext.SaveChangesAsync();
        }

        
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<Response> UserLogin(User userLogin)
        {
            if (userLogin is null)
                return (new Response() { seccess = false, message = "Bad Request Made!"});
               
            //search about user if exist
            var user = await _appDbContext.Users.Where(x => x.UserName.ToLower().Equals(userLogin.UserName.ToLower())).FirstOrDefaultAsync();
            //search about user if exist
            var userRole = await _appDbContext.UserRoles.Where(_ => _.UserId == user!.UserID).FirstOrDefaultAsync();


            if (user is null || userRole is null)
                return (new Response() { seccess = false, message = "Invalid User/Password" });

            bool doPasswordMatch = VerifyUserPassword(userLogin.UserPassword, user.UserPassword);

            if (!doPasswordMatch)
                return (new Response() { seccess = false, message = "Invalid User/Password" });

             

            return GenerateJWTToken(user.UserName, userRole.Role);

        }
        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(string id, User user)
        {
             
            User? editUser = _appDbContext.Users.FirstOrDefault(x => x.UserID == Guid.Parse(id));

            editUser.UserName = user.UserName;
            editUser.UserPassword = user.UserPassword;

            _appDbContext.SaveChangesAsync();
        }
        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            var userResult = _appDbContext.Users.Where(x => x.UserID == Guid.Parse(id)).FirstOrDefault();
            _appDbContext.Users.Remove(userResult);
             _appDbContext.SaveChangesAsync();

        }


        //Cheack The Password to Compare with database password method
        private bool VerifyUserPassword(string rawPassword,string databasePassword)
        {
            byte[] dbPasswordHash = Convert.FromBase64String(databasePassword);

            byte[] salt = new byte[16];
            Array.Copy(dbPasswordHash,0,salt,0,16);
            var rfcPassord = new Rfc2898DeriveBytes(rawPassword, salt, 1000, HashAlgorithmName.SHA1);
            byte[] rfcPasswordHash = rfcPassord.GetBytes(20);

            for (int i = 0; i < rfcPasswordHash.Length; i++)
            {
                if (dbPasswordHash[i+16] != rfcPasswordHash[i])
                {
                    return false;
                }
            }
            return true;
        }

        //Generate JWT Token
        private Response GenerateJWTToken(  string userName, string role)
        {

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claimsIdentity = new ClaimsIdentity(new List<Claim>
            { 
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.Role, role),
            });

            var securityTokenDiscriptor = new SecurityTokenDescriptor
            {
                 Issuer = _config["Jwt:Issuer"],
                 Audience = _config["Jwt:Audience"],
                Subject = claimsIdentity,
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials,
            };

            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtTokenHandler.CreateToken(securityTokenDiscriptor);
            var token = jwtTokenHandler.WriteToken(securityToken);

            return new Response()
            {
                Role = role,
                Username = userName,
                Token = token,
                seccess = true
            };
        }
    }
}
