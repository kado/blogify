using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogifyWebApi.Models.Interfaces
{
    public interface IComment
    {

        public int Id { get; set; }
        public int BlogId { get; set; }
        public DateTime Created { get; set; }
        public string Data { get; set; }
        public string Author { get; set; }

        

    }
}
