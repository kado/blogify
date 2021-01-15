using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BlogifyWebApp.Models.Interfaces;

namespace BlogifyWebApp.Models
{
    //2021-01-13 - Kadel D. Lacatt
    //View Model for exchage blog data between views, implements IBlog
    public class BlogCreateViewModel : IBlog 
    {
        public int Id { get; set; }
        public int Category { get; set; }
        public string Title { get; set; }
        public DateTime Created { get; set; }
        public string Author { get; set; }
        public string Data { get; set; }
        public string Editor { get; set; }
        public DateTime? Edited { get; set; }
        public string Revision { get; set; }
        public string Status { get; set; }

    }
}
