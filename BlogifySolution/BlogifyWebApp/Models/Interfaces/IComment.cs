using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogifyWebApp.Models.Interfaces
{
    //2021-01-14 Kadel D Lacatt
    //Interface for abstracting Comment class for DI purposes
    public interface IComment
    {

        public int Id { get; set; }
        public int BlogId { get; set; }
        public DateTime Created { get; set; }
        public string Data { get; set; }
        public string Author { get; set; }

        

    }
}
