using Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Models.Enum;


namespace HospitalManagementAndAppointmentSystem.DTOs
{
    public class PatientRegistrationDto  
    {
        [Required]
        public  string UserName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 8)]
        public string Password { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Phone]
        public int PhoneNumber { get; set; }
        [Required]
        public string EmergencyContactName { get; set; }
        [Required]
        public Relationship EmergencyContactRelationship { get; set; }
        [Required]
        public string? EmergencyContactPhoneNumber { get; set; }

        public DateTime DateOfBirth { get; set; }
        [Required]
        public PatientGender Gender { get; set; }
        public AppointmentType AppointmentBookedBy { get; set; }
        

    }
}



