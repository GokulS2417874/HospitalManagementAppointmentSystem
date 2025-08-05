namespace Infrastructure.DTOs
{
    public class CreatePaymentDto
    {
        public int AppointmentId { get; set; }
        public decimal Amount { get; set; }
        public string TransactionId { get; set; } = string.Empty;
        public Domain.Models.Enum.PaymentMethod PaymentMethod { get; set; }
    }
}
