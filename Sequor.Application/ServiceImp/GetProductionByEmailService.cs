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

        public async Task<IEnumerable<GetProductionResponseDTO>> Execute(string email)
        {
            var productions = await _productionRepository.GetByEmailAsync(email);

            // Garante que sempre retorna lista, nunca null
            if (productions == null || !productions.Any())
                return new List<GetProductionResponseDTO>();

            return _mapper.Map<IEnumerable<GetProductionResponseDTO>>(productions);
        }

    }
}
