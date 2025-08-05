using Microsoft.AspNetCore.Http;
using static Domain.Models.Enum;

namespace Infrastructure.DTOs
{
    public class AppointmentDto
    {
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public DateOnly AppointmentDate { get; set; }
        public TimeOnly? AppointmentStartTime { get; set; }
        public TimeOnly? AppointmentEndTime { get; set; }
        public bool Submit { get; set; } = false;
        public IFormFile? FilePath { get; set; }

        public BookedBy BookedBy { get; set; }

        public int? HelpDeskId { get; set; }


    }
    public class DoctorAppointmentUpdateDto
    {
        public bool IsAttended { get; set; }
    }
}
