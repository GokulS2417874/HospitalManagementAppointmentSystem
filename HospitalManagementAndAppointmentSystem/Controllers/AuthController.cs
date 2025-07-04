using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementAndAppointmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromForm] string username, [FromForm] string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return BadRequest("Invalid credentials");
            }

            //// Generate JWT Token
            //var tokenHandler = new JwtSecurityTokenHandler();
            //var key = _configuration.GetValue<string>("ApiSettings:Secret");
            //var keyBytes = Encoding.ASCII.GetBytes(key);
            //var tokenDescriptor = new SecurityTokenDescriptor
            //{
            //    Subject = new ClaimsIdentity(new Claim[]
            //    {
            //        new Claim(ClaimTypes.Name, username)
            //    }),
            //    Expires = DateTime.UtcNow.AddHours(1),
            //    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
            //};
            //var token = tokenHandler.CreateToken(tokenDescriptor);
            //var tokenString = tokenHandler.WriteToken(token);


            // Check DB Object and validate
            TokenGeneration jwtTokenString = new TokenGeneration(_configuration);
            string tokenString = jwtTokenString.GenerateJWT(username, "Admin", "Asset", "All", "en");
            return Ok(new { Token = tokenString });
        }
        }
    }
