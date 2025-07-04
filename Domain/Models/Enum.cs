using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public abstract class Enum
    {
        public enum Status
        {
            Online,
            Busy,
            Offline
        }
        public enum PatientGender
        {
            Female,
            Male
        }
        public enum Relationship
        {
            Father,
            Mother,
            Guardian,
            Spouse,
            Others
        }
        public enum UserRole
        {
            Patient,
            Doctor,
            HelpDesk,
            Admin,
        }
        public enum ScheduleTime
        {
            Morning,
            Afternoon,
            Night
        }
        public enum AppointmentStatus
        {
            Pending,
            Confirmed,
            Cancelled,
            Completed,
            Rescheduled
        }
        public enum NotificationType
        {
            info,
            warning,
            alert,
            success
        }
        public enum AppointmentType
        {
            Self,
            HelpDesk
        }
    }
}

