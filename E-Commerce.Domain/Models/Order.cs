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

        //public List<Product> OrderProducts { get; set; } = new List<Product>();

        public double TotalPrice { get; set; }
        public decimal Price { get; set; }

        public int Quantity { get; set; }




         
      public List<OrderProduct> OrderProducts { get; set; } = new();


      
        }


   
}
