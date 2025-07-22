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
