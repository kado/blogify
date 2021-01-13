using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BlogifyWebApp.Models
{
    public class LoginViewModel
    {

        public string Username { get; set; }
        public string Password { get; set; }
    }
}
