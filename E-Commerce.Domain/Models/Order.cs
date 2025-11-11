using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Models
{
    public class Order : BaseModel
    {
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public string Status { get; set; }
        public DateTime OrderDate { get; set; }

        public List<Product> Products { get; set; } = new List<Product>();

        public double TotalPrice { get; set; }
  
 

     }
}
