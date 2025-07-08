using Infrastructure.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementAndAppointmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IPasswordHasher _hash;

        public AuthController(IAuthRepository repo, IPasswordHasher hash)
        {
            _repo = repo;
            _hash = hash;
           
        }
    }
}
