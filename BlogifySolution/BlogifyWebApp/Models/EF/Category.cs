using System;
using System.Collections.Generic;


using BlogifyWebApp.Models.Interfaces;

#nullable disable

namespace BlogifyWebApp.Models.EF
{
    //2021-01-13 - Kadel D. Lacatt
    //EF Category entity points to db table Category
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
