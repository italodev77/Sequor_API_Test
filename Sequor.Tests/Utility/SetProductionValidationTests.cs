using Moq;
using Sequor.Application.DTOs;
using Sequor.Application.IRepositories;
using Sequor.Application.Utility;
using Sequor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Sequor.Application.Tests.Utility
{
    public class SetProductionValidationTests
    {
        private readonly Mock<IUserRepository> _userRepoMock = new();
        private readonly Mock<IOrderRepository> _orderRepoMock = new();

        private SetProductionValidation CreateValidator()
            => new SetProductionValidation(_userRepoMock.Object, _orderRepoMock.Object);

        private SetProductionRequestDTO DefaultRequest() => new SetProductionRequestDTO
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
        public async Task Should_Fail_When_User_Is_Not_Registered()
        {
            //arrange
            var request = DefaultRequest();
            _userRepoMock.Setup(r => r.GetByEmailAsync(request.Email)).ReturnsAsync((User)null);
            var validator = CreateValidator();

            //act
            var result = await validator.ValidateAsync(request);

            //assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Usuário não cadastrado!", result.Message);
        }

        [Fact]
        public async Task Should_Fail_When_Order_Is_Not_Registered()
        {
            //arrange
            var request = DefaultRequest();
            _userRepoMock.Setup(r => r.GetByEmailAsync(request.Email))
                .ReturnsAsync(new User { InitialDate = DateTime.Now.AddDays(-1), EndDate = DateTime.Now.AddDays(1) });
            _orderRepoMock.Setup(r => r.GetByIdAsync(request.Order)).ReturnsAsync((Order)null);
            var validator = CreateValidator();

            //act
            var result = await validator.ValidateAsync(request);

            //assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Ordem não cadastrada!", result.Message);
        }

        [Fact]
        public async Task Should_Fail_When_Date_Is_Invalid()
        {
            //arrange
            var request = DefaultRequest();
            request.ProductionDate = "invalid_date";
            _userRepoMock.Setup(r => r.GetByEmailAsync(request.Email))
                .ReturnsAsync(new User { InitialDate = DateTime.Now.AddDays(-1), EndDate = DateTime.Now.AddDays(1) });
            _orderRepoMock.Setup(r => r.GetByIdAsync(request.Order))
                .ReturnsAsync(new Order { Quantity = 100, Product = new Product { ProductMaterials = new List<ProductMaterial>() } });
            var validator = CreateValidator();

            //act
            var result = await validator.ValidateAsync(request);

            //assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Data inválida!", result.Message);
        }

        [Fact]
        public async Task Should_Fail_When_Date_Is_Out_Of_User_Period()
        {
            //arrange
            var request = DefaultRequest();
            _userRepoMock.Setup(r => r.GetByEmailAsync(request.Email))
                .ReturnsAsync(new User { InitialDate = DateTime.Now.AddDays(-10), EndDate = DateTime.Now.AddDays(-5) });
            _orderRepoMock.Setup(r => r.GetByIdAsync(request.Order))
                .ReturnsAsync(new Order { Quantity = 100, Product = new Product { ProductMaterials = new List<ProductMaterial>() } });
            var validator = CreateValidator();

            //act
            var result = await validator.ValidateAsync(request);

            //assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Data fora do período permitido!", result.Message);
        }

        [Fact]
        public async Task Should_Fail_When_Quantity_Is_Invalid()
        {
            //arrange
            var request = DefaultRequest();
            request.Quantity = -1;
            _userRepoMock.Setup(r => r.GetByEmailAsync(request.Email))
                .ReturnsAsync(new User { InitialDate = DateTime.Now.AddDays(-1), EndDate = DateTime.Now.AddDays(1) });
            _orderRepoMock.Setup(r => r.GetByIdAsync(request.Order))
                .ReturnsAsync(new Order { Quantity = 100, Product = new Product { ProductMaterials = new List<ProductMaterial>() } });
            var validator = CreateValidator();

            //act
            var result = await validator.ValidateAsync(request);

            //assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Quantidade inválida!", result.Message);
        }

        [Fact]
        public async Task Should_Fail_When_Material_Is_Not_Registered()
        {
            //arrange
            var request = DefaultRequest();
            _userRepoMock.Setup(r => r.GetByEmailAsync(request.Email))
                .ReturnsAsync(new User { InitialDate = DateTime.Now.AddDays(-1), EndDate = DateTime.Now.AddDays(1) });
            _orderRepoMock.Setup(r => r.GetByIdAsync(request.Order))
                .ReturnsAsync(new Order { Quantity = 100, Product = new Product { ProductMaterials = new List<ProductMaterial>() } });
            var validator = CreateValidator();

            //act
            var result = await validator.ValidateAsync(request);

            //assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Material não cadastrado para o produto!", result.Message);
        }

        [Fact]
        public async Task Should_Fail_When_CycleTime_Is_Invalid()
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
                    Product = new Product
                    {
                        ProductMaterials = new List<ProductMaterial>
                        {
                            new ProductMaterial { MaterialCode = "MAT01" }
                        }
                    }
                });
            var validator = CreateValidator();

            //act
            var result = await validator.ValidateAsync(request);

            //assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Tempo de ciclo inválido!", result.Message);
        }

        [Fact]
        public async Task Should_Pass_With_Warning_When_CycleTime_Is_Less_Than_Product()
        {
            //arrange
            var request = DefaultRequest();
            request.CycleTime = 5;
            _userRepoMock.Setup(r => r.GetByEmailAsync(request.Email))
                .ReturnsAsync(new User { InitialDate = DateTime.Now.AddDays(-1), EndDate = DateTime.Now.AddDays(1) });
            _orderRepoMock.Setup(r => r.GetByIdAsync(request.Order))
                .ReturnsAsync(new Order
                {
                    Quantity = 100,
                    Product = new Product
                    {
                        CycleTime = 10,
                        ProductMaterials = new List<ProductMaterial>
                        {
                            new ProductMaterial { MaterialCode = "MAT01" }
                        }
                    }
                });
            var validator = CreateValidator();

            //act
            var result = await validator.ValidateAsync(request);

            //assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Atenção: Tempo de ciclo menor que o cadastrado no produto.", result.ExtraMessage);
        }
    }
}
