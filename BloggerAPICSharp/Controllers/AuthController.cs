using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BloggerAPICSharp.DataLayer.DomainModel;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BloggerAPICSharp.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly SignInManager<ApplicationUser> signInMgr;
        private readonly UserManager<ApplicationUser> userMgr;
        private readonly IPasswordHasher<ApplicationUser> hasher;
        private readonly IConfigurationRoot config;

        public AuthController(SignInManager<ApplicationUser> _signInMgr,
            UserManager<ApplicationUser> _userMgr,
            IPasswordHasher<ApplicationUser> _hasher,
            IConfigurationRoot _config)
        {
            signInMgr = _signInMgr;
            userMgr = _userMgr;
            hasher = _hasher;
            config = _config;
        }
        // GET: auth/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST auth/register
        [HttpPost("register")]
        public void Post([FromBody]string value)
        {
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]ApplicationUser model)
        {
            try
            {
                var user = await userMgr.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    if (hasher.VerifyHashedPassword(user,user.PasswordHash,model.Password)== PasswordVerificationResult.Success)
                    {
                        var claims = new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                           new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                        };
                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Token:Key"]));
                        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var  token = new JwtSecurityToken(
                            issuer: config["Token:Issuer"],
                            audience: config["Token:Audience"],
                            claims: claims,
                            expires: DateTime.UtcNow.AddMinutes(30),
                            signingCredentials:creds
                            );
                        return Ok(
                        new
                        {
                            token =new JwtSecurityTokenHandler().WriteToken(token),
                            expiration= token.ValidTo
                        });
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return BadRequest("user name or password are incorrect");

        }


        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
