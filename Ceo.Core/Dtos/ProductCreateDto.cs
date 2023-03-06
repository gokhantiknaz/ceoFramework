﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ceo.Core.Dtos
{
    public class ProductCreateDto
    {

        public string Name { get; set; }

        public int Stock { get; set; }

        public decimal Price { get; set; }

        public int CategoryId { get; set; }
    }
}
