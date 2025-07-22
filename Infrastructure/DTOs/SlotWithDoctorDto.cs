namespace Infrastructure.DTOs
{
    public class SlotWithDoctorDto : Domain.Models.Enum
    {
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public specialization? Specialization { get; set; }
        public ShiftTime Shift { get; set; }
        public List<SlotDto> Slots { get; set; }    
    }
}
