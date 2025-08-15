using Sequor.Application.DTOs;
using Sequor.Application.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sequor.Application.Services.Interfaces
{
    public interface ISetProductionService
    {
        Task<Result<SetProductionResponseDTO>> SetProductionAsync(SetProductionRequestDTO request);
    }
}
