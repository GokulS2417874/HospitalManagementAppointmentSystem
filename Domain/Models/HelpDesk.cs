using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class HelpDesk : Users
    {
        public HelpDesk()
        {
            Role = UserRole.HelpDesk;
        }
        public int MaxDailyBookings { get; set; }
        public Status Active_Status { get; set; }
    }

}


