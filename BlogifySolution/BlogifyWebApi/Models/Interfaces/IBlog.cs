using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogifyWebApi.Models.Interfaces
{
    //2021-01-14 - Kadel D. Lacatt
    //Interface for abstracting Blog entity and use DI
    public interface IBlog
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
