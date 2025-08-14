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
    public class GetOrdersServiceImp: IGetOrdersService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetOrdersServiceImp(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<GetOrdersResponseDTO> GetAllOrders()
        {
            var orders = await _orderRepository.GetAllAsync();
            var dtoList = _mapper.Map<IEnumerable<OrderDTO>>(orders);
            return new GetOrdersResponseDTO { Orders = dtoList };
        }
    }
}
