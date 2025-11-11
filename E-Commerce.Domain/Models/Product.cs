using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Models
{
    public class Product: BaseModel
    {
         public string Name { get; set; }
        public string Description { get; set; }

        public double price { get; set; }
        public int stock { get; set; }

    }
}
