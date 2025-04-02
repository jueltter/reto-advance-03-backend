using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using reto_advance_03_backend.Entities;

namespace reto_advance_03_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenAuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        // private readonly UserManager<IdentityUser> _userManager;

        /*
        public TokenAuthController(IConfiguration config, UserManager<IdentityUser> userManager)
        {
            _config = config;
            _userManager = userManager;
        }
        */

        public TokenAuthController(IConfiguration config)
        {
            _config = config;
        }


        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] ProgramUser programUser) { 
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, programUser.Username),
                new Claim(ClaimTypes.Email, programUser.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("correct horse battery staple correct horse battery staple correct horse battery staple"));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(authClaims),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token),
                expires = token.ValidTo
            });
        }



    }
}
