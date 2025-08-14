using Sequor.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sequor.Application.ProductionFactory
{
    public static class SetProductionResponseFactory
    {
        public static SetProductionResponseDTO Success(string message)
        {
            return new SetProductionResponseDTO
            {
                Status = 200,
                Type = "S",
                Description = message
            };
        }

        public static SetProductionResponseDTO Error(string message)
        {
            return new SetProductionResponseDTO
            {
                Status = 201,
                Type = "E",
                Description = $"Falha no apontamento - {message}"
            };
        }
    }
}
