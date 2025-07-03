using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Models.Enum;

namespace Domain.Models
{
    public class Notification
    {
        public int NotificationId { get; set; }
        public NotificationType Title { get; set; }
        public string? Message { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public Appointment Appointment{ get; set; }
    }

}
