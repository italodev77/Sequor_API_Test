using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sequor.Application.DTOs
{
    public class OrderDTO
    {
        public string Order { get; set; } = null!;
        public decimal Quantity { get; set; }
        public string ProductCode { get; set; } = null!;
        public string ProductDescription { get; set; } = null!;
        public string? Image { get; set; }
        public decimal CycleTime { get; set; }
        public IEnumerable<MaterialDTO> Materials { get; set; } = new List<MaterialDto>();
    }
}
