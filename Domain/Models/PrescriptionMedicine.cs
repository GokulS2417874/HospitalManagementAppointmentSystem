using System.Text.Json.Serialization;
using static Domain.Models.Enum;

namespace Domain.Models
{
    public class PrescriptionMedicine
    {
        public int Id { get; set; }
        public int PrescriptionId { get; set; }
        public string MedicineType { get; set; }
        public string Dosages { get; set; }

        [JsonIgnore]  // Add this to prevent circular reference
        public Prescription Prescription { get; set; }

        public string ScheduleTime { get; set; }
    }
}