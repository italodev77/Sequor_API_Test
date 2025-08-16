using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sequor.Application.DTOs
{
    public class ProductionItemDTO
    {
        public string Order { get; set; } = null!;
        public string Date { get; set; }
        public decimal Quantity { get; set; }
        public string MaterialCode { get; set; } = null!;
        public decimal CycleTime { get; set; }
    }
}
