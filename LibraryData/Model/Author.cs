﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryData.Model
{
    public class Author : LibItem
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string OtherNames { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime DateOfDeath { get; set; }

        public string Address { get; set; }

        public string Summary { get; set; }
    }
}
