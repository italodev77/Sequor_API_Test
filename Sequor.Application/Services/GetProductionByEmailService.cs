using AutoMapper;
using Sequor.Application.DTOs;
using Sequor.Application.IRepositories;
using Sequor.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sequor.Application.Services
{
    public class GetProductionByEmailService: IGetProductionByEmailService
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

            var response = new GetProductionResponseDTO
            {
                Productions = _mapper.Map<IEnumerable<ProductionItemDTO>>(productions)
            };

            return response;
        }
        
    }



    
}
