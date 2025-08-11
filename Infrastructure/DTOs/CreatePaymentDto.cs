using static Domain.Models.Enum;

namespace Infrastructure.DTOs
{
    public class CreatePaymentDto
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}
