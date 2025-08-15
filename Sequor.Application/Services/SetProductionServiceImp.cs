using Sequor.Application.DTOs;
using Sequor.Application.IRepositories;
using Sequor.Application.Result;
using Sequor.Application.Services.Interfaces;
using Sequor.Application.Utility;
using Sequor.Domain.Entities;
using System.Threading.Tasks;

namespace Sequor.Application.Services
{
    public class SetProductionServiceImp : ISetProductionService
    {
        private readonly IProductionRepository _productionRepository;
        private readonly SetProductionValidation _validator;

        public SetProductionServiceImp(
            IOrderRepository orderRepository,
            IProductionRepository productionRepository,
            SetProductionValidation validator)
        {
            _productionRepository = productionRepository;
            _validator = validator;
        }

        public async Task<Result<SetProductionResponseDTO>> SetProductionAsync(SetProductionRequestDTO request)
        {
            var validation = await _validator.ValidateAsync(request);

          
            Result<SetProductionResponseDTO> Fail(string message)
            {
                var errorResponse = new SetProductionResponseDTO
                {
                    Status = 201,
                    Type = "E",
                    Description = message
                };
                return Result<SetProductionResponseDTO>.Failure(errorResponse, message);
            }

            if (!validation.IsSuccess)
                return Fail(validation.Message);

            var production = new Production
            {
                Email = request.Email,
                OrderId = request.Order,
                Date = validation.Data,
                Quantity = request.Quantity,
                MaterialCode = request.MaterialCode,
                CycleTime = request.CycleTime
            };

            await _productionRepository.AddAsync(production);

           
            var successResponse = new SetProductionResponseDTO
            {
                Status = 200,
                Type = "S",
                Description = string.IsNullOrWhiteSpace(validation.ExtraMessage)
                    ? "Apontamento realizado com sucesso!"
                    : $"Apontamento realizado com sucesso! {validation.ExtraMessage}"
            };

            return Result<SetProductionResponseDTO>.Success(successResponse);
        }
    }
}
