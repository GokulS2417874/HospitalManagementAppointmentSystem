using Infrastructure.Interface;

namespace Infrastructure.Repository
{
    public class BcryptPasswordHasher : IPasswordHasher
    {
        public string Hash(string p)
        {
            return BCrypt.Net.BCrypt.HashPassword(p);
        }

        public bool Verify(string inputPassword,string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(inputPassword, hashedPassword);
        }

        
    }
}
