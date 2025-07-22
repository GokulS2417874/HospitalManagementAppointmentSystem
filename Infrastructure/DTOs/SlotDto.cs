namespace Infrastructure.DTOs
{
    public class SlotDto : Domain.Models.Enum
    {
        public TimeOnly? StartTime { get; set; }
        public TimeOnly? EndTime { get; set; }
        public ShiftTime? Shift { get; set; }
        public bool IsBooked { get; set; } = false;
        public SlotStatus Status { get; set; }
    }
}

