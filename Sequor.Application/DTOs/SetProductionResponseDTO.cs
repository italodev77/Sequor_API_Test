using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sequor.Application.DTOs
{
    public class SetProductionResponseDTO
    {
        public int Status { get; set; }
        public string Type { get; set; } = null!;
        public string Description { get; set; } = null!;
    }

}
