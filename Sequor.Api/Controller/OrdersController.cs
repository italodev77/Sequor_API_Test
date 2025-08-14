using Microsoft.AspNetCore.Mvc;
using Sequor.Application.DTOs;
using Sequor.Application.IService;

namespace Sequor.Api.Controller
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IGetOrdersService _getOrdersService;
        private readonly IGetProductionByEmailService _getProductionByEmailService;
        private readonly ISetProductionService _setProductionService;

        public OrdersController(
            IGetOrdersService getOrdersService,
            IGetProductionByEmailService getProductionByEmailService,
            ISetProductionService setProductionService)
        {
            _getOrdersService = getOrdersService;
            _getProductionByEmailService = getProductionByEmailService;
            _setProductionService = setProductionService;
        }

        /// <summary>
        /// Retorna todas as ordens.
        /// </summary>
        [HttpGet("GetOrders")]
        public async Task<ActionResult<GetOrdersResponseDTO>> GetOrders()
        {
            var result = await _getOrdersService.GetAllOrders();
            return Ok(result);
        }

        /// <summary>
        /// Retorna produções filtradas por e-mail.
        /// </summary>
        [HttpGet("GetProduction")]
        public async Task<ActionResult<GetProductionResponseDTO>> GetProduction([FromQuery] string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return BadRequest(new { message = "Email é obrigatório" });

            var result = await _getProductionByEmailService.Execute(email);
            return Ok(result);
        }

        /// <summary>
        /// Registra uma produção seguindo as regras de negócio.
        /// </summary>
        [HttpPost("SetProduction")]
        public async Task<IActionResult> SetProduction([FromBody] SetProductionRequestDTO request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _setProductionService.SetProductionAsync(request);
            return Ok(response);
        }
    }
}
