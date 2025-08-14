using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sequor.Application.DTOs
{
    public class SetProductionRequestDTO
    {
        public string Email { get; set; } = null!;
        public string Order { get; set; } = null!;
        public string ProductionDate { get; set; } = null!;
        public string ProductionTime { get; set; } = null!;
        public decimal Quantity { get; set; }
        public string MaterialCode { get; set; } = null!;
        public decimal CycleTime { get; set; }
    }   
}
