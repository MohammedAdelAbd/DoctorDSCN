using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SharedLibrary.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebApplication2.Data;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        private AppDbContext _AppDbContext;
        public LoginController(IConfiguration config, AppDbContext appDbContext)
        {
            _config = config;
            _AppDbContext = appDbContext;
        }
        


        

        [HttpPost]
        public IActionResult Post(User loginRequest)
        {
            //your logic for login process
            //If login usrename and password are correct then proceed to generate token
            var userCur = _AppDbContext.Users
                .Where(x => x.UserName == loginRequest.UserName && x.UserPassword == loginRequest.UserPassword).FirstOrDefault();

            if (userCur != null)
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, userCur.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.NameIdentifier, userCur.UserID.ToString()),
                    // Add other claims here as needed
                    new Claim(ClaimTypes.Role, "UserRole") // Example role claim
                };

                var Sectoken = new JwtSecurityToken(_config["Jwt:Issuer"],
                  _config["Jwt:Audience"],
                  claims: claims,
                  expires: DateTime.Now.AddDays(2),
                  signingCredentials: credentials);

                var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);

                return Ok(token);
            }

            return NotFound();
            
        }
    }
}
