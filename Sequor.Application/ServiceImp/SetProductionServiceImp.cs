using Sequor.Application.DTOs;
using Sequor.Application.IRepositories;
using Sequor.Application.IService;
using Sequor.Application.ProductionFactory;
using Sequor.Application.Utility;
using Sequor.Application.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sequor.Application.ServiceImp
{
    public class SetProductionServiceImp: ISetProductionService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductionRepository _productionRepository;
        private readonly IUserRepository _userRepository;
        private readonly SetProductionValidator _validator;
        private readonly SetProductionFactory _factory;

        public SetProductionServiceImp(
            IOrderRepository orderRepository,
            IProductionRepository productionRepository,
            IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _productionRepository = productionRepository;
            _userRepository = userRepository;
            _validator = new SetProductionValidator(userRepository, orderRepository);
            _factory = new SetProductionFactory();
        }

        public async Task<SetProductionResponseDTO> SetProductionAsync(SetProductionRequestDTO request)
        {
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
                return SetProductionResponseFactory.Error(validationResult.ErrorMessage);

            var production = _factory.Create(request, validationResult.ProdDateTime!.Value);

            await _productionRepository.AddAsync(production);

            return SetProductionResponseFactory.Success("Apontamento realizado com sucesso!" + validationResult.ExtraMessage);
        }
    }
}
