using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogifyWebApp.Models.Interfaces
{
    //2021-01-14 Kadel D Lacatt
    //Interface for abstracting AuthProvider class for DI purposes
    public interface IAuthProvider
    {
        public bool VerifyUser(string username, string password);
        public IUser GetUser(string username);
        
    }
}
