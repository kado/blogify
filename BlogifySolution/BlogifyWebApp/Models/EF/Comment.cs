﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


using BlogifyWebApp.Models.Interfaces;
#nullable disable

namespace BlogifyWebApp.Models.EF
{
    //2021-01-13 - Kadel D. Lacatt
    //EF Comment entity points to db table Comment
    public partial class Comment : IComment
    {
        public int Id { get; set; }
        public int BlogId { get; set; }
        public DateTime Created { get; set; }

        [MaxLength(200, ErrorMessage="Comment too long. Please shorten it. ")]        
        [Display(Name="Comment")]
        public string Data { get; set; }
        public string Author { get; set; }

        public virtual Blog Blog { get; set; }
    }
}
