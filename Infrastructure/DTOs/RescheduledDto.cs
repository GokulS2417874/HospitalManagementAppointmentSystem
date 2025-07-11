using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
namespace Infrastructure.DTOs
{
    public class RescheduledDto: Domain.Models.Enum
    {
        public DateOnly AppointmentDate { get; set; }
        public TimeOnly? AppointmentStartTime { get; set; }
        public TimeOnly? AppointmentEndTime { get; set; }
        //public AppointmentStatus AppointmentStatus { get; set; }

    }
}
