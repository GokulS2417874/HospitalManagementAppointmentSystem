using Domain.Models;
using Infrastructure.DTOs;
using Infrastructure.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using static Domain.Models.Enum;
using Enum = Domain.Models.Enum;

namespace HospitalManagementAndAppointmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IPasswordHasher _hash;

        public RegistrationController(IAuthRepository repo, IPasswordHasher hash)
        {
            _repo = repo;
            _hash = hash;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] GenericRegistrationForm form)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _repo.FindByEmailAsync(form.Email) is not null)
                return BadRequest("Email already registered");

            if (form.Role == UserRole.Admin)
            {
                var existingAdmin = await _repo.GetAdminAsync();
                if (existingAdmin != null)
                    return BadRequest("An Admin is already registered. Only one Admin is allowed.");
            }

            Users? user = form.Role 
            switch
            {
                UserRole.Admin => new Admin
                {
                    Email = form.Email,
                    PasswordHash = _hash.Hash(form.Password),
                    Role = UserRole.Admin
                },

                UserRole.Doctor => new Doctor
                {
                    UserName = form.UserName,
                    Email = form.Email,
                    PasswordHash = _hash.Hash(form.Password),
                    PhoneNumber = form.PhoneNumber,
                    EmergencyContactName = form.EmergencyContactName,
                    EmergencyContactRelationship = form.EmergencyContactRelationship,
                    EmergencyContactPhoneNumber = form.EmergencyContactPhoneNumber,
                    Specialization = form.Specialization,
                    Qualification = form.Qualification,
                    ExperienceYears = form.ExperienceYears ?? 0,
                    Shift = form.Shift,
                    Role = UserRole.Doctor
                },

                UserRole.Patient => new Patient
                {
                    UserName = form.UserName,
                    Email = form.Email,
                    PasswordHash = _hash.Hash(form.Password),
                    PhoneNumber = form.PhoneNumber,
                    EmergencyContactName = form.EmergencyContactName,
                    EmergencyContactRelationship = form.EmergencyContactRelationship,
                    EmergencyContactPhoneNumber = form.EmergencyContactPhoneNumber,
                    DateOfBirth = form.DateOfBirth,
                    Gender = form.Gender ?? PatientGender.Others,
                    AppointmentBookedBy = form.AppointmentBookedBy ?? AppointmentType.Self,
                    Role = UserRole.Patient
                },

                UserRole.HelpDesk => new HelpDesk
                {
                    UserName = form.UserName,
                    Email = form.Email,
                    PasswordHash = _hash.Hash(form.Password),
                    PhoneNumber = form.PhoneNumber,
                    EmergencyContactName = form.EmergencyContactName,
                    EmergencyContactRelationship = form.EmergencyContactRelationship,
                    EmergencyContactPhoneNumber = form.EmergencyContactPhoneNumber,
                    Languages = form.Languages,
                    Qualification = form.Qualification,
                    Role = UserRole.HelpDesk
                },
            };

            if (user == null)
                return BadRequest("Invalid role or missing required fields");

            await _repo.AddUserAsync(user);
            await _repo.SaveAsync();

            return Ok($"{user.Role} registered successfully");
        }
    }
}
