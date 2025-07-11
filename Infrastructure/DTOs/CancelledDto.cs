using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Infrastructure.DTOs
{
    public class CancelledDto : Domain.Models.Enum
    {
        public string Email { get; set;}
        public AppointmentStatus AppointmentStatus { get; set; }
    }
}
