using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Models
{
    public enum OrderStatus
    {
        Pending = 0,
        Confirmed = 1,
        Shipped = 2,
        Delivered = 3
    }

}
