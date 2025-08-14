using Sequor.Application.DTOs;
using Sequor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sequor.Application.ProductionFactory
{
    public class SetProductionFactory
    {
        public Production Create(SetProductionRequestDTO request, DateTime prodDateTime)
        {
            return new Production
            {
                Email = request.Email,
                OrderId = request.Order,
                Date = prodDateTime,
                Quantity = request.Quantity,
                MaterialCode = request.MaterialCode,
                CycleTime = request.CycleTime
            };
        }
    }
}
