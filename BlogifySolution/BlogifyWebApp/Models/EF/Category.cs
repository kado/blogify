using System;
using System.Collections.Generic;


using BlogifyWebApp.Models.Interfaces;

#nullable disable

namespace BlogifyWebApp.Models.EF
{
    public partial class Category : ICategory
    {
        public Category()
        {
            Blogs = new HashSet<Blog>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Blog> Blogs { get; set; }
        
    }
}
