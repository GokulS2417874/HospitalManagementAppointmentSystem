using Microsoft.Extensions.Primitives;
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
            Male,
            Others
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
        public enum ShiftTime
        {
            Morning,
            Afternoon,
            Night
        }
        public enum AppointmentStatus
        {
            InProgress,
            Scheduled,
            Cancelled,
            Completed,
            Rescheduled,
            NotAttended
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
        public enum specialization
        {
            Cardiologist,
            Dermatologist,
            Neurologist,
            Orthopedic_Surgeon,
            Pediatrician,
            Psychiatrist,
            Ophthalmologist,
            ENT_Specialist,
            Gastroenterologist,
            Urologist,
            Endocrinologist,
            Oncologist
        }
        public enum PaymentMethod
        {
            Card,
            Cash,
            Upi,
            NetBanking
        }
        public enum TabletScheduleTime
        {
            Morning,
            Afternoon,
            Evening,
            Night,
            AfterMeal,
            BeforeMeal
        }
        public enum MedicineType
        {
            Paracetamol,       
            Ibuprofen,        
            Aspirin,           
            Naproxen,          
            Diclofenac,        
            Tramadol,
            Morphine,          
            Codeine,          
            Amitriptyline,     
            Effervescent,      
            GelForm,           
            SyrupForm,       
            Inhaler,           
            Suppository        
        }
    }

}

