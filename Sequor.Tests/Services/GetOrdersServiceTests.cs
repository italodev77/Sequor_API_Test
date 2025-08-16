using AutoMapper;
using Moq;
using Sequor.Application.DTOs;
using Sequor.Application.IRepositories;
using Sequor.Application.Services;
using Sequor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sequor.Tests.Services
{
    public class GetOrdersServiceTests
    {
        private readonly Mock<IOrderRepository> _orderRepoMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly GetOrdersServiceImp _service;

        public GetOrdersServiceTests()
        {
            _orderRepoMock = new Mock<IOrderRepository>();
            _mapperMock = new Mock<IMapper>();
            _service = new GetOrdersServiceImp(_orderRepoMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetAllOrders_ShouldReturnMappedOrders()
        {
            //arrange
            var orders = new List<Order> { new Order { OrderId = "123", Quantity = 10 } };
            var orderDtos = new List<OrderDTO> { new OrderDTO { Order = "123", Quantity = 10 } };

            _orderRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(orders);
            _mapperMock.Setup(m => m.Map<IEnumerable<OrderDTO>>(orders)).Returns(orderDtos);

            //act
            var result = await _service.GetAllOrders();

            //assert
            Assert.NotNull(result);
            Assert.Single(result.Orders);
            Assert.Equal("123", result.Orders.First().Order);
        }
    }
}
