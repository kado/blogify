using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

using BlogifyWebApp.Models.Interfaces;


namespace BlogifyWebApp.Models
{
    //2021-01-13 - Kadel D. Lacatt
    //View Model for exchage List of blog data between views, implements IBlog
    public class ListBlogViewModel
    {
        [Display(Name = "Category Filter")]
        public int categoryId { get; set; }
        public IEnumerable<SelectListItem> BlogCategories;
        public IEnumerable<IBlog> BlogEntries;
    }
}
