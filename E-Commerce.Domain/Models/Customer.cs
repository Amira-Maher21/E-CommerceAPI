using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Models
{
    public class Customer: BaseModel
    {
         public string Name { get; set; }

        public string email { get; set; }

        public string Phone { get; set; }


    }
}
