using Sequor.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sequor.Application.IService
{
    internal interface IGetProductionByEmailService
    {
        Task<GetProductionResponseDTO> Execute(string email);
    }
}
