namespace Domain.Models
{
    public class Patient : Users
    {
        public Patient()
        {
            Role = UserRole.Patient;
        }
    }
}


   