﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogifyWebApp.Models.Interfaces
{
    public interface ICategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
                
    }
}
