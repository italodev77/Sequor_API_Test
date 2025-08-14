using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sequor.Application.DTOs
{
    public class GetProductionResponseDTO
    {
        public IEnumerable<ProductionItemDTO> Productions { get; set; } = new List<ProductionItemDTO>();
    }

}
