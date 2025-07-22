namespace Infrastructure.DTOs
{
    public class RescheduledDto: Domain.Models.Enum
    {
        public DateOnly AppointmentDate { get; set; }
        public TimeOnly? AppointmentStartTime { get; set; }
        public TimeOnly? AppointmentEndTime { get; set; }
    }
}
