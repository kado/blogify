using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


using BlogifyWebApi.Models.Interfaces;
#nullable disable

namespace BlogifyWebApi.Models.EF
{
    //2021-01-14 - Kadel D. Lacatt
    //EF Comment points to db table Comment
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
