using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public string? Languages { get; set; }
  
        public string? Qualification { get; set; }
        public Status? Active_Status { get; set; }
    }

}


