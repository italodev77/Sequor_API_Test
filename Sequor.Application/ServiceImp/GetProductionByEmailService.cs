using AutoMapper;
using Sequor.Application.DTOs;
using Sequor.Application.IRepositories;
using Sequor.Application.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sequor.Application.ServiceImp
{
    internal class GetProductionByEmailService: IGetProductionByEmailService
    {
        private readonly IProductionRepository _productionRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetProductionByEmailService(
            IProductionRepository productionRepository,
            IOrderRepository orderRepository,
            IMapper mapper)
        {
            _productionRepository = productionRepository;
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<GetProductionResponseDTO> Execute(string email)
        {
            var productions = await _productionRepository.GetByEmailAsync(email);
            var dtoList = _mapper.Map<IEnumerable<ProductionItemDTO>>(productions);
            return new GetProductionResponseDTO { Productions = dtoList };
        }

    }
}
