using BloggerAPICSharp.DataLayer.DomainModel;
using BloggerAPICSharp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BloggerAPICSharp.Manangers
{
    public class AuthManager
    {
        private readonly SignInManager<ApplicationUser> signInMgr;
        private readonly UserManager<ApplicationUser> userMgr;
        private readonly IPasswordHasher<ApplicationUser> hasher;
        private readonly IConfigurationRoot config;
        public AuthManager(SignInManager<ApplicationUser> _signInMgr,
            UserManager<ApplicationUser> _userMgr,
            IPasswordHasher<ApplicationUser> _hasher,
            IConfigurationRoot _config)
        {
            signInMgr = _signInMgr;
            userMgr = _userMgr;
            hasher = _hasher;
            config = _config;
        }

        public async Task<TokenViewModel> LoginManager(ApplicationUser model)
        {
            var user = await userMgr.FindByNameAsync(model.UserName);
            if (user != null)
            {
                if (hasher.VerifyHashedPassword(user, user.PasswordHash, model.Password) == PasswordVerificationResult.Success)
                {
                    var claims = new[]
                    {
                           new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                           new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                     };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Token:Key"]));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token =  new JwtSecurityToken(
                        issuer: config["Token:Issuer"],
                        audience: config["Token:Audience"],
                        claims: claims,
                        expires: DateTime.UtcNow.AddMinutes(30),
                        signingCredentials: creds
                        );
                    return new TokenViewModel()
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo
                    };
                }
            }
            return null;
        }
    }
}
