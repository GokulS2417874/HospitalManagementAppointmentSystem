using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Infrastructure.DTOs
{
    public class SlotDto : Domain.Models.Enum
    {
        public TimeOnly? StartTime { get; set; }
        public TimeOnly? EndTime { get; set; }
        public ShiftTime? Shift { get; set; }
        public bool IsBooked { get; set; } = false;
        public Status Status { get; set; }
            //get
            //{
            //    var now = TimeOnly.FromDateTime(DateTime.Now);
            //    if (IsBooked)
            //        return Status.Busy;
            //    else if (EndTime <= now)
            //        return Status.Offline;
            //    else
            //        return Status.Online;
            //}
           
        }
    }

