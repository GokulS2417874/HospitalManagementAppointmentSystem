using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Doctor : Users
    {
        public Doctor()
        {
            Role = UserRole.Doctor;
        }
        public string? Specialization { get; set; }
        public string? Qualification { get; set; }
        public int? ExperienceYears { get; set; }
        public Status? Active_Status { get; set; }
        public int? Consultant_fees { get; set; } = 500;
    }
}
