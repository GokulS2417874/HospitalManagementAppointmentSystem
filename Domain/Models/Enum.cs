namespace Domain.Models
{
    public abstract class Enum
    {
        public enum Status
        {
            Online,
            Busy,
            Offline,
            Not_Available
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
            Night,
            NotAllocated
        }
        public enum AppointmentStatus
        {
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
            none =0  ,
            Paracetamol   = 1,       
            Ibuprofen = 2,        
            Aspirin = 4,           
            Naproxen = 8,          
            Diclofenac = 16,        
            Tramadol = 32,
            Morphine =64,          
            Codeine = 128,          
            Amitriptyline = 256,     
            Effervescent = 512,      
            GelForm = 1024,           
            SyrupForm = 2048,       
            Inhaler = 4096,           
            Suppository = 8192       
        }

        [Flags]
        public enum DosageType
        {
            None = 0,
            Mg_100 = 1,
            Mg_200 = 2,
            Mg_300 = 4,
            Mg_400 = 8,
            Mg_500 = 16,
            Mg_600 = 32,
            Mg_700 = 64,
            Mg_800 = 128,
            Mg_900 = 256,
            Mg_1000 = 512
        }

        public enum SlotStatus
        {
            Not_Available,
            Busy,
            Available
        }
        public enum AdminApproval
        {
            Pending,
            Approved,
            NotApproved
        }

    }

}
