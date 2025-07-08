using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Models.Enum;

namespace Domain.Models
{
    public class Admin : Users
    {
        public Admin()
        {
            Role = UserRole.Admin;
        }
    }
}
