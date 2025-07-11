using Domain;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public  class Users : Enum
    {
        [Key]
        public int UserId { get; set; }
      
        public string? UserName { get; set; }
        [Required]
        [MaxLength(100)]
        public string PasswordHash { get; set; }
        public string? ResetToken { get; set; } = null;
        public DateTime? ResetTokenExpiry { get; set; } = null;
        [Required]
        [EmailAddress]
        [MaxLength(60)]
        public string Email { get; set; }
         public string? PhoneNumber { get; set; }
        public UserRole? Role { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? DeletedAt { get; set; } = DateTime.Now;
        public string? EmergencyContactName { get; set; }  = null;
        public Relationship? EmergencyContactRelationship { get; set; }
        public string? EmergencyContactPhoneNumber { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public PatientGender? Gender { get; set; }
        public AppointmentType? AppointmentBookedBy { get; set; }
        public specialization? Specialization { get; set; }
        public string? Qualification { get; set; }
        public int? ExperienceYears { get; set; }
        public Status? Active_Status { get; set; } = Status.Offline;
        public int? Consultant_fees { get; set; } = 500;
        public string? Languages { get; set; }
        public ShiftTime? Shift { get; set; }
    }
}

  