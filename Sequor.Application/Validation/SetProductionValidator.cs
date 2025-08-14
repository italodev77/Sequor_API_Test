using Sequor.Application.DTOs;
using Sequor.Application.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sequor.Application.Validation
{
    public class SetProductionValidator
    {
        private readonly IUserRepository _userRepository;
        private readonly IOrderRepository _orderRepository;

        public SetProductionValidator(IUserRepository userRepository, IOrderRepository orderRepository)
        {
            _userRepository = userRepository;
            _orderRepository = orderRepository;
        }

        public async Task<ValidationResult> ValidateAsync(SetProductionRequestDTO request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null)
                return ValidationResult.Fail("Usuário não cadastrado!");

            var order = await _orderRepository.GetByIdAsync(request.Order);
            if (order == null)
                return ValidationResult.Fail("Ordem não cadastrada!");

            if (!DateTime.TryParse($"{request.ProductionDate} {request.ProductionTime}", out var prodDateTime))
                return ValidationResult.Fail("Data inválida!");

            if (prodDateTime < user.InitialDate || prodDateTime > user.EndDate)
                return ValidationResult.Fail("Data fora do período permitido!");

            if (request.Quantity <= 0 || request.Quantity > order.Quantity)
                return ValidationResult.Fail("Quantidade inválida!");

            if (!order.Product.ProductMaterials.Any(pm => pm.MaterialCode == request.MaterialCode))
                return ValidationResult.Fail("Material não cadastrado para o produto!");

            string extraMessage = "";
            if (request.CycleTime <= 0)
                return ValidationResult.Fail("Tempo de ciclo inválido!");

            if (request.CycleTime < order.Product.CycleTime)
                extraMessage = " Atenção: Tempo de ciclo menor que o cadastrado no produto.";

            return ValidationResult.Success(prodDateTime, extraMessage);
        }
    }
}
