using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BlogifyWebApp.Models.Interfaces;

namespace BlogifyWebApp.Models
{
    //2021-01-13 - Kadel D. Lacatt
    //View Model for exchage blog data between views, implements 
    public class BlogDetailViewModel
    {
        public IBlog blog { get; set; } //blog data
        public IEnumerable<IComment> comments { get; set; } //List of comments
    }
}
