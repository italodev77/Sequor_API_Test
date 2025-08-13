using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sequor.Domain.Entities
{
    public class Production
    {
        public long Id { get; set; }
        public string Email { get; set; } = null!;
        public string OrderId { get; set; } = null!;
        public DateTime Date { get; set; }
        public decimal Quantity { get; set; }
        public string MaterialCode { get; set; } = null!;
        public decimal CycleTime { get; set; }
    }
}
