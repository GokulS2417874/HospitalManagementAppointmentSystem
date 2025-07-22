using Domain.Data;
using Infrastructure.DTOs;
using Infrastructure.Interface;
using Microsoft.AspNetCore.Mvc;


namespace HospitalManagementAndAppointmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class LoginController : Controller
    {
        private readonly IAuthRepository _repo;
        private readonly IPasswordHasher _hash;
        private readonly AppDbContext _context;
        public readonly TokenGeneration _tok;
        public readonly IEmailSender _email;
        public LoginController(IAuthRepository repo, IPasswordHasher hash,AppDbContext context,TokenGeneration tok,IEmailSender email)
        {
            _repo = repo;
            _hash = hash; 
            _context = context;
            _tok = tok;
            _email = email;
            
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromForm] LoginDto dto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var Password =await _repo.FindPassword(dto.Email);
            if (Password == null) 
                return BadRequest("Email Not Found"); 
            else if ((_hash.Verify(dto.Password, Password.ToString()) == false))
                return BadRequest("Enter Valid Password"); 
            var user = _context.Users.Where(x => x.Email == dto.Email).FirstOrDefault();
            var Token = _tok.GenerateJWT(user);
            return Ok(new { Token = Token, Role = user.Role.ToString()});

        }
        [HttpPost("forgot")]
        public async Task<IActionResult> Forgot([FromForm] ForgotPasswordDto dto)
        {
            var user = await _repo.FindByEmailAsync(dto.Email);
            if (user == null)
            {
                return NotFound();
            }

            user.ResetToken = Guid.NewGuid().ToString();
            user.ResetTokenExpiry = DateTime.Now.AddMinutes(30);
            await _repo.SaveAsync();

            //var link = $"{dto.FrontendBaseUrl}?token={user.ResetToken}";
            //await _email.SendResetLinkAsync(user.Email, body);

            var body = $"https://front/reset?token={user.ResetToken}";

            await _email.SendResetLinkAsync(user.Email, body);

            return Ok(
                new
                {

                    message = "Reset link sent",
                    token = user.ResetToken,
                    body
                }

                );
        }
        [HttpPost("reset")]
        public async Task<IActionResult> Reset([FromForm]ResetPasswordDto dto)
        {
            var user = await _repo.FindByResetTokenAsync(dto.Token);

            if (user == null || user.ResetTokenExpiry < DateTime.Now)
            {
                return BadRequest("Invalid or Expired token");
            }

            user.PasswordHash = _hash.Hash(dto.NewPassword);
            user.ResetToken = null;
            user.ResetTokenExpiry = null;
            await _repo.SaveAsync();
            return Ok("Password updated");
        }

    }
}
