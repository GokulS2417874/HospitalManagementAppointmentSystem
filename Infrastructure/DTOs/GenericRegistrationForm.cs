using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using static Domain.Models.Enum;

namespace Infrastructure.DTOs
{
    public class GenericRegistrationForm
    {
        [Required]
        public UserRole Role { get; set; }
        public string? UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public PatientGender? Gender { get; set; }
        public string? PhoneNumber { get; set; }
        public string? EmergencyContactName { get; set; }
        public Relationship? EmergencyContactRelationship { get; set; }
        public string? EmergencyContactPhoneNumber { get; set; }
        public specialization? Specialization { get; set; }
        public string? Qualification { get; set; }
        public int? ExperienceYears { get; set; }
        public string? Languages { get; set; }
        public IFormFile? ProfileImage { get; set; }

        public BookedBy? BookedBy { get; set; }
        // Who booked the registration
        public int? HelpDeskId { get; set; }







        }
    }
