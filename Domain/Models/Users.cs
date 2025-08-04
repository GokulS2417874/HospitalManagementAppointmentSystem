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
        public byte[]? ProfileImage { get; set; }
        public string? ProfileImageMimeType { get; set; }
        public string? ProfileImageFileName { get; set; }

        public specialization? Specialization { get; set; }
        public string? Qualification { get; set; }  
        public int? ExperienceYears { get; set; }
        public Status? Active_Status { get; set; } = Status.Offline;
        public int? Consultant_fees { get; set; } = 500;
        public string? Languages { get; set; }
        public ShiftTime? Shift { get; set; } = ShiftTime.NotAllocated;
        public TimeOnly? ShiftStartTime
        {
            get
            {
                if (Shift.Equals(ShiftTime.Morning))
                {
                    return new TimeOnly(5,0);
                }
                else
                {
                    return Shift.Equals(ShiftTime.Afternoon) ? new TimeOnly(13,0) : Shift.Equals(ShiftTime.Night) ? new TimeOnly(21,0) : new TimeOnly(0 , 0);
                }
            }
        }

        public TimeOnly? ShiftEndTime
        {
            get
            {
                if (Shift.Equals(ShiftTime.Morning))
                {
                    return new TimeOnly(13,0);
                }
                else
                {
                    return Shift.Equals(ShiftTime.Afternoon) ? new TimeOnly(21,0) : Shift.Equals(ShiftTime.Night) ? new TimeOnly(5, 0) : new TimeOnly(0, 0);
                }
            }
           }
        public AppointmentType? RegisteredBy { get; set; } = AppointmentType.Self;
        public AdminApproval IsApprovedByAdmin { get; set; } = AdminApproval.Pending;
        public ICollection<Payment> Payments { get; set; }
    }
}

  