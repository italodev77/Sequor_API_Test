using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sequor.Application.DTOs
{
    public class GetOrdersResponseDTO
    {
        public IEnumerable<OrderDto> Orders { get; set; } = new List<OrderDto>();
    }
}
