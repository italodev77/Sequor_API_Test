using Sequor.Application.DTOs;
using Sequor.Application.IRepositories;
using Sequor.Application.Result;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sequor.Application.Utility
{
    public class SetProductionValidation
    {
        private readonly IUserRepository _userRepository;
        private readonly IOrderRepository _orderRepository;

        public SetProductionValidation(IUserRepository userRepository, IOrderRepository orderRepository)
        {
            _userRepository = userRepository;
            _orderRepository = orderRepository;
        }

        public async Task<Result<DateTime>> ValidateAsync(SetProductionRequestDTO request)
        {
           
            Result<DateTime> Fail(string message) => Result<DateTime>.Failure(default, message);

            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null)
                return Fail("Usuário não cadastrado!");

            var order = await _orderRepository.GetByIdAsync(request.Order);
            if (order == null)
                return Fail("Ordem não cadastrada!");

            if (!DateTime.TryParse($"{request.ProductionDate} {request.ProductionTime}", out var prodDateTime))
                return Fail("Data inválida!");

            if (prodDateTime < user.InitialDate || prodDateTime > user.EndDate)
                return Fail("Data fora do período permitido!");

            if (request.Quantity <= 0 || request.Quantity > order.Quantity)
                return Fail("Quantidade inválida!");

            if (!order.Product.ProductMaterials.Any(pm => pm.MaterialCode == request.MaterialCode))
                return Fail("Material não cadastrado para o produto!");

            if (request.CycleTime <= 0)
                return Fail("Tempo de ciclo inválido!");

            string extraMessage = "";
            if (request.CycleTime < order.Product.CycleTime)
                extraMessage = "Atenção: Tempo de ciclo menor que o cadastrado no produto.";

            return Result<DateTime>.Success(prodDateTime, extraMessage: extraMessage);
        }
    }
}
