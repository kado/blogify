using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogifyWebApp.Models.Interfaces
{
    public interface IAuthProvider
    {
        public bool VerifyUser(string username, string password);
        public IUser GetUser(string username);
        
    }
}
