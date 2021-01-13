using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BlogifyWebApp.Models.Interfaces;

namespace BlogifyWebApp.Models
{
    public class BlogDetailViewModel
    {
        public IBlog blog { get; set; }
        public IEnumerable<IComment> comments { get; set; }
    }
}
