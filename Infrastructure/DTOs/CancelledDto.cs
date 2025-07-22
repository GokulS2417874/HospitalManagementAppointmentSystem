namespace Infrastructure.DTOs
{
    public class CancelledDto : Domain.Models.Enum
    {
        public string Email { get; set;}
        public AppointmentStatus AppointmentStatus { get; set; }
    }
}
