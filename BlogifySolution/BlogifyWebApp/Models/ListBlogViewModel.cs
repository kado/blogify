using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

using BlogifyWebApp.Models.Interfaces;


namespace BlogifyWebApp.Models
{
    public class ListBlogViewModel
    {
        [Display(Name = "Category Filter")]
        public int categoryId { get; set; }
        public IEnumerable<SelectListItem> BlogCategories;
        public IEnumerable<IBlog> BlogEntries;
    }
}
