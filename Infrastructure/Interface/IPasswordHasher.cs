using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interface
{
    public interface IPasswordHasher
    {
        string Hash(string palin);

        bool Verify(string palin, string hash);

    }
}
