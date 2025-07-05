using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Models.Enum;

namespace Infrastructure.DTOs
{
    public class DoctorRegistrationDto 
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [StringLength(20,MinimumLength =8)]
        public string Password { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        public string EmergencyContactName { get; set; }
        [Required]
        public Relationship EmergencyContactRelationship { get; set; }
        [Required]
        public string EmergencyContactPhoneNumber { get; set; }
        [Required]
        public string Specialization { get; set; }
        [Required]
        public string Qualification { get; set; }
        [Required]
        public int ExperienceYears { get; set; }
    }
}
