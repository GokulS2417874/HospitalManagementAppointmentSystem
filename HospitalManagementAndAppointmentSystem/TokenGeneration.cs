using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration.UserSecrets;
using Domain.Models;

namespace HospitalManagementAndAppointmentSystem
{

    public class TokenGeneration : Users
    {
        public readonly IConfiguration _config;
        public TokenGeneration(IConfiguration config)
        {
            _config = config;
        }
        public string GenerateJWT(Users user)
        {


            var Key = _config.GetValue<string>("ApiSettings:Secret");
            var SecuredKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
            var SecurityCredential = new SigningCredentials(SecuredKey, SecurityAlgorithms.HmacSha256);
            var Claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.UserId.ToString()),
                new Claim(ClaimTypes.Email,user.Email.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };
            var Token = new JwtSecurityToken(
                issuer: "Admin",
               audience: "Users",
               claims: Claims,
               expires: DateTime.Now.AddHours(2),
               signingCredentials: SecurityCredential);
            var Tokens = new JwtSecurityTokenHandler();
            return Tokens.WriteToken(Token);

        }


    }
}

