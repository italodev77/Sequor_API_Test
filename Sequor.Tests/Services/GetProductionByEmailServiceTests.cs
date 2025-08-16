using Xunit;
using Moq;
using AutoMapper;
using Sequor.Application.DTOs;
using Sequor.Application.IRepositories;
using Sequor.Application.Services;
using Sequor.Domain.Entities;

namespace Sequor.Tests
{
    public class GetProductionByEmailServiceTests
    {
        private readonly Mock<IProductionRepository> _prodRepoMock;
        private readonly Mock<IOrderRepository> _orderRepoMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly GetProductionByEmailService _service;

        public GetProductionByEmailServiceTests()
        {
            _prodRepoMock = new Mock<IProductionRepository>();
            _orderRepoMock = new Mock<IOrderRepository>();
            _mapperMock = new Mock<IMapper>();

            _service = new GetProductionByEmailService(
                _prodRepoMock.Object,
                _orderRepoMock.Object,
                _mapperMock.Object
            );
        }

        [Fact]
        public async Task Execute_ShouldReturnProductionsByEmail()
        {
            //arrange
            var productions = new List<Production>
            {
                new Production
                {
                    OrderId = "ORD001",
                    Date = new DateTime(2025, 8, 16, 14, 30, 0),
                    Quantity = 10,
                    MaterialCode = "MAT001",
                    CycleTime = 5
                }
            };

            var expectedDate = "8/16/2025 02:30:00 PM";

            var productionDtos = new List<ProductionItemDTO>
            {
                new ProductionItemDTO
                {
                    Order = "ORD001",
                    Date = expectedDate,
                    Quantity = 10,
                    MaterialCode = "MAT001",
                    CycleTime = 5
                }
            };

            _prodRepoMock.Setup(r => r.GetByEmailAsync("teste@email.com"))
                         .ReturnsAsync(productions);

            _mapperMock.Setup(m => m.Map<IEnumerable<ProductionItemDTO>>(productions))
                       .Returns(productionDtos);

            //act
            var result = await _service.Execute("teste@email.com");

            //assert
            Assert.NotNull(result);
            Assert.Single(result.Productions);
            Assert.Equal("ORD001", result.Productions.First().Order);
            Assert.Equal("MAT001", result.Productions.First().MaterialCode);
            Assert.Equal(10, result.Productions.First().Quantity);
            Assert.Equal(5, result.Productions.First().CycleTime);
            Assert.Equal(expectedDate, result.Productions.First().Date);
        }
    }
}
