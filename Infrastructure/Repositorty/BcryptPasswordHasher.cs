using Infrastructure.Interface;

namespace JWT.Repository
{
    public class BcryptPasswordHasher : IPasswordHasher
    {
        public string Hash(string p)
        {
            return BCrypt.Net.BCrypt.HashPassword(p);
        }

        public bool Verify(string p, string h)
        {
            return BCrypt.Net.BCrypt.Verify(p, h);
        }
    }
}
