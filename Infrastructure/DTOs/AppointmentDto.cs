using Microsoft.AspNetCore.Http;


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

    }
    public class DoctorAppointmentUpdateDto
    {
        public bool IsAttended { get; set; }
    }
}
