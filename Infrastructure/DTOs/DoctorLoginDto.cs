using System.ComponentModel.DataAnnotations;

namespace Infrastructure.DTOs
{
    public class DoctorLoginDto
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string? Password {  get; set; }
    }
}
