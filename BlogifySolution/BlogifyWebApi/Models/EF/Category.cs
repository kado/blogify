using System;
using System.Collections.Generic;


using BlogifyWebApi.Models.Interfaces;

#nullable disable
//2021-01-14 - Kadel D. Lacatt
//EF Category points to db table Category
namespace BlogifyWebApi.Models.EF
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
