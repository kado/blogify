using System;
using System.Collections.Generic;

using BlogifyWebApi.Models.Interfaces;

#nullable disable
//2021-01-14 - Kadel D. Lacatt
//EF Blog points to db table Blog
namespace BlogifyWebApi.Models.EF
{
    public partial class Blog : IBlog
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

        public virtual User AuthorNavigation { get; set; }
        public virtual Category CategoryNavigation { get; set; }
        public virtual User EditorNavigation { get; set; }
    }
}
