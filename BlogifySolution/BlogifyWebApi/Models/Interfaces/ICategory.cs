using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogifyWebApi.Models.Interfaces
{
    //2021-01-14 - Kadel D. Lacatt
    //Interface for abstracting Category entity and use DI
    public interface ICategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
                
    }
}
