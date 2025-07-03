using Domain;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
   
    public abstract class Users : Enum
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 8)]
        public string Password { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(60)]
        public string EmailId { get; set; }
        [Required]
        [Phone]
        [MaxLength(10)]
        public string PhoneNumber { get; set; }
        public UserRole Role { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? DeletedAt { get; set; } = DateTime.Now;
        [Required]
        public string? EmergencyContactName { get; set; }
        [Required]
        public Relationship EmergencyContactRelationship { get; set; }
        [Required]
        public string? EmergencyContactPhoneNumber { get; set; }


    }
}

  