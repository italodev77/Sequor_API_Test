using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sequor.Domain.Entities
{
    public class Order
    {
        public string OrderId { get; set; } = null!; // "Order" in PDF
        public decimal Quantity { get; set; }
        public string ProductCode { get; set; } = null!;
        public Product Product { get; set; } = null!;
    }
}
