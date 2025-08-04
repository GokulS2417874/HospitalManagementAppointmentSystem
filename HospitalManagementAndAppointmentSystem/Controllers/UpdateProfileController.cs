using Infrastructure.DTOs;
using Infrastructure.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static Domain.Models.Enum;
using Microsoft.AspNetCore.Authorization;

namespace HospitalManagementAndAppointmentSystem.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Doctor,Patient,Admin,HelpDesk")]

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
        [HttpGet("{id}/profile-image")]
        public async Task<IActionResult> GetProfileImage(int id)
        {
            var user = await _userRepository.FindByIdAsync(id); // You need to implement this in IUserRepository

            if (user == null || user.ProfileImage == null)
                return NotFound("User or profile image not found");

            return File(user.ProfileImage, user.ProfileImageMimeType, user.ProfileImageFileName);
        }

    }
}
