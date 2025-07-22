namespace Domain.Models
{
    public class HelpDesk : Users
    {
        public HelpDesk()
        {
            Role = UserRole.HelpDesk;
        }
    }
}


