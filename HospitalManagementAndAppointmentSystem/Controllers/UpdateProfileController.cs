using Infrastructure.DTOs;
using Infrastructure.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace HospitalManagementAndAppointmentSystem.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    
    public class UpdateProfileController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UpdateProfileController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("GetAllUsers")]

        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsersAsync();

            return Ok(users);
        }

        [HttpPut("UpdateProfile")]
        public async Task<IActionResult> UpdateProfile([FromForm] GenericRegistrationForm dto)
        {
            var emailClaim = User.FindFirst(ClaimTypes.Email)?.Value;

            if (string.IsNullOrWhiteSpace(emailClaim))
            {
                return BadRequest("Email claim not found in token.");
            }

            var result = await _userRepository.UpdateUserProfileAsync(emailClaim, dto);

            if (result == "User not found.")
                return NotFound(result);

            return Ok(result);
        }

        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser()
        {
            var emailClaim = User.FindFirst(ClaimTypes.Email)?.Value;

            var result = await _userRepository.DeleteUserAsync(emailClaim);

            if (result == null)
                return NotFound("User not Found");

            return Ok(result);
        }

    }
}
