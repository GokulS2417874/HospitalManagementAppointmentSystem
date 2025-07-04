using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace HospitalManagementAndAppointmentSystem.Controllers
{
    public class SecuredController : ControllerBase
    {
        [HttpGet("securedendpoint")]
        public IActionResult GetSecureData()
        {
            return Ok("This is a secure endpoint. Only authorized users can access this.");
        }

        [Authorize]
        [HttpGet("profile")]
        public IActionResult GetSignedData()
        {
            //Extract from Payload 
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            //Custom Claims in Payload 
            var departmentName = User.FindFirst("Department")?.Value;
            var accessLevel = User.FindFirst("accessLevel")?.Value;
            var perferred_language = User.FindFirst("_en_")?.Value;
            var tokenGotExpired = User.FindFirst(JwtRegisteredClaimNames.Exp)?.Value;

            //Validate an extracted value... 
            if (departmentName == "Finance")
            {
                //Do This 
            }
            else
            {
                //do that
            }

            //Fetch Token ID 
            var TokenID = User.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;
            //if (IsTokenBlacklisted(TokenID)) { 
            //   retun Unautho
            //}
            return Ok(new { Role = role, Department = departmentName, AccessLevel = accessLevel, PreferredLanguage = perferred_language, tokenExpiry = tokenGotExpired, TokenID = TokenID });
        }
    }
}
