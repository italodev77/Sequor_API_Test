using Moq;
using Sequor.Application.DTOs;
using Sequor.Application.IRepositories;
using Sequor.Application.Result;
using Sequor.Application.Services;
using Sequor.Application.Utility;
using Sequor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Sequor.Application.Tests.Services
{
    public class SetProductionServiceImpTests
    {
        private readonly Mock<IProductionRepository> _productionRepoMock = new();
        private readonly Mock<IOrderRepository> _orderRepoMock = new();
        private readonly Mock<IUserRepository> _userRepoMock = new();

        private SetProductionServiceImp CreateService()
        {
            var validator = new SetProductionValidation(_userRepoMock.Object, _orderRepoMock.Object);
            return new SetProductionServiceImp(_orderRepoMock.Object, _productionRepoMock.Object, validator);
        }

        private SetProductionRequestDTO DefaultRequest() => new()
        {
            Email = "user@test.com",
            Order = "ORD123",
            ProductionDate = "2025-08-16",
            ProductionTime = "08:00",
            Quantity = 10,
            MaterialCode = "MAT01",
            CycleTime = 5
        };

        [Fact]
        public async Task Should_Return_Error_When_User_Is_Not_Registered()
        {
            //arrange
            var request = DefaultRequest();
            _userRepoMock.Setup(r => r.GetByEmailAsync(request.Email)).ReturnsAsync((User)null);
            var service = CreateService();

            //act
            var result = await service.SetProductionAsync(request);

            //assert
            Assert.False(result.IsSuccess);
            Assert.Equal(201, result.Data.Status);
            Assert.Equal("E", result.Data.Type);
            Assert.Contains("Usuário não cadastrado", result.Data.Description);
        }

        [Fact]
        public async Task Should_Return_Error_When_Order_Is_Not_Registered()
        {
            //arrange
            var request = DefaultRequest();
            _userRepoMock.Setup(r => r.GetByEmailAsync(request.Email))
                .ReturnsAsync(new User { InitialDate = DateTime.Now.AddDays(-1), EndDate = DateTime.Now.AddDays(1) });
            _orderRepoMock.Setup(r => r.GetByIdAsync(request.Order)).ReturnsAsync((Order)null);
            var service = CreateService();

            //act
            var result = await service.SetProductionAsync(request);

            //assert
            Assert.False(result.IsSuccess);
            Assert.Equal(201, result.Data.Status);
            Assert.Equal("E", result.Data.Type);
            Assert.Contains("Ordem não cadastrada", result.Data.Description);
        }

        [Fact]
        public async Task Should_Return_Error_When_Date_Is_Invalid()
        {
            //arrange
            var request = DefaultRequest();
            request.ProductionDate = "invalid_date";
            _userRepoMock.Setup(r => r.GetByEmailAsync(request.Email))
                .ReturnsAsync(new User { InitialDate = DateTime.Now.AddDays(-1), EndDate = DateTime.Now.AddDays(1) });
            _orderRepoMock.Setup(r => r.GetByIdAsync(request.Order))
                .ReturnsAsync(new Order { Quantity = 100, Product = new Product { ProductMaterials = new List<ProductMaterial>() } });
            var service = CreateService();

            //act
            var result = await service.SetProductionAsync(request);

            //assert
            Assert.False(result.IsSuccess);
            Assert.Equal(201, result.Data.Status);
            Assert.Equal("E", result.Data.Type);
            Assert.Contains("Data inválida", result.Data.Description);
        }

        [Fact]
        public async Task Should_Return_Error_When_Quantity_Is_Invalid()
        {
            //arrange
            var request = DefaultRequest();
            request.Quantity = -5;
            _userRepoMock.Setup(r => r.GetByEmailAsync(request.Email))
                .ReturnsAsync(new User { InitialDate = DateTime.Now.AddDays(-1), EndDate = DateTime.Now.AddDays(1) });
            _orderRepoMock.Setup(r => r.GetByIdAsync(request.Order))
                .ReturnsAsync(new Order
                {
                    Quantity = 10,
                    Product = new Product { ProductMaterials = new List<ProductMaterial> { new() { MaterialCode = "MAT01" } } }
                });
            var service = CreateService();

            //act
            var result = await service.SetProductionAsync(request);

            //assert
            Assert.False(result.IsSuccess);
            Assert.Equal(201, result.Data.Status);
            Assert.Equal("E", result.Data.Type);
            Assert.Contains("Quantidade inválida", result.Data.Description);
        }

        [Fact]
        public async Task Should_Return_Error_When_Material_Is_Not_Registered()
        {
            //arrange
            var request = DefaultRequest();
            _userRepoMock.Setup(r => r.GetByEmailAsync(request.Email))
                .ReturnsAsync(new User { InitialDate = DateTime.Now.AddDays(-1), EndDate = DateTime.Now.AddDays(1) });
            _orderRepoMock.Setup(r => r.GetByIdAsync(request.Order))
                .ReturnsAsync(new Order
                {
                    Quantity = 100,
                    Product = new Product { ProductMaterials = new List<ProductMaterial>() }
                });
            var service = CreateService();

            //act
            var result = await service.SetProductionAsync(request);

            //assert
            Assert.False(result.IsSuccess);
            Assert.Equal(201, result.Data.Status);
            Assert.Equal("E", result.Data.Type);
            Assert.Contains("Material não cadastrado", result.Data.Description);
        }

        [Fact]
        public async Task Should_Return_Error_When_CycleTime_Is_Invalid()
        {
            //arrange
            var request = DefaultRequest();
            request.CycleTime = 0;
            _userRepoMock.Setup(r => r.GetByEmailAsync(request.Email))
                .ReturnsAsync(new User { InitialDate = DateTime.Now.AddDays(-1), EndDate = DateTime.Now.AddDays(1) });
            _orderRepoMock.Setup(r => r.GetByIdAsync(request.Order))
                .ReturnsAsync(new Order
                {
                    Quantity = 100,
                    Product = new Product { ProductMaterials = new List<ProductMaterial> { new() { MaterialCode = "MAT01" } }, CycleTime = 10 }
                });
            var service = CreateService();

            //act
            var result = await service.SetProductionAsync(request);

            //assert
            Assert.False(result.IsSuccess);
            Assert.Equal(201, result.Data.Status);
            Assert.Equal("E", result.Data.Type);
            Assert.Contains("Tempo de ciclo inválido", result.Data.Description);
        }

        [Fact]
        public async Task Should_Return_Success_When_All_Valid()
        {
            //arrange
            var request = DefaultRequest();
            _userRepoMock.Setup(r => r.GetByEmailAsync(request.Email))
                .ReturnsAsync(new User { InitialDate = DateTime.Now.AddDays(-1), EndDate = DateTime.Now.AddDays(1) });
            _orderRepoMock.Setup(r => r.GetByIdAsync(request.Order))
                .ReturnsAsync(new Order
                {
                    Quantity = 100,
                    Product = new Product { ProductMaterials = new List<ProductMaterial> { new() { MaterialCode = "MAT01" } }, CycleTime = 5 }
                });
            var service = CreateService();

            //act
            var result = await service.SetProductionAsync(request);

            //assert
            Assert.True(result.IsSuccess);
            Assert.Equal(200, result.Data.Status);
            Assert.Equal("S", result.Data.Type);
            Assert.Contains("Apontamento realizado com sucesso", result.Data.Description);
            _productionRepoMock.Verify(r => r.AddAsync(It.IsAny<Production>()), Times.Once);
        }

        [Fact]
        public async Task Should_Return_Success_With_Warning_When_CycleTime_Less_Than_Product()
        {
            //arrange
            var request = DefaultRequest();
            request.CycleTime = 3;
            _userRepoMock.Setup(r => r.GetByEmailAsync(request.Email))
                .ReturnsAsync(new User { InitialDate = DateTime.Now.AddDays(-1), EndDate = DateTime.Now.AddDays(1) });
            _orderRepoMock.Setup(r => r.GetByIdAsync(request.Order))
                .ReturnsAsync(new Order
                {
                    Quantity = 100,
                    Product = new Product { ProductMaterials = new List<ProductMaterial> { new() { MaterialCode = "MAT01" } }, CycleTime = 5 }
                });
            var service = CreateService();

            //act
            var result = await service.SetProductionAsync(request);

            //assert
            Assert.True(result.IsSuccess);
            Assert.Equal(200, result.Data.Status);
            Assert.Equal("S", result.Data.Type);
            Assert.Contains("Atenção: Tempo de ciclo menor que o cadastrado no produto", result.Data.Description);
            _productionRepoMock.Verify(r => r.AddAsync(It.IsAny<Production>()), Times.Once);
        }
    }
}
