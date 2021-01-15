using System;
using System.Collections.Generic;
using BlogifyWebApp.Models.Interfaces;

#nullable disable

namespace BlogifyWebApp.Models.EF
{
    //2021-01-13 - Kadel D. Lacatt
    //EF User entity points to db table User
    public partial class User : IUser
    {
        public User()
        {
            BlogAuthorNavigations = new HashSet<Blog>();
            BlogEditorNavigations = new HashSet<Blog>();
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Blog> BlogAuthorNavigations { get; set; }
        public virtual ICollection<Blog> BlogEditorNavigations { get; set; }
    }
}
