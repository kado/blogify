using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogifyWebApp.Models.Interfaces
{
    //2021-01-14 Kadel D Lacatt
    //Interface for abstracting Category class for DI purposes
    public interface ICategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
                
    }
}
