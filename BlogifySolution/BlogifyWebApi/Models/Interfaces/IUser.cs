using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogifyWebApi.Models.Interfaces
{
    //2021-01-14 - Kadel D. Lacatt
    //Interface for abstracting User entity and use DI
    public interface IUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }

    }
}
