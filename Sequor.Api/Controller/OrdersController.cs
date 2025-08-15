using Microsoft.AspNetCore.Mvc;
using Sequor.Application.DTOs;
using Sequor.Application.Services.Interfaces;
using System.Threading.Tasks;

namespace Sequor.Api.Controller
{
    [ApiController]
    [Route("api/orders")]
    [Produces("application/json")]
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
        [ProducesResponseType(typeof(GetOrdersResponseDTO), 200)]
        [ProducesResponseType(typeof(BadRequestObjectResult), 400)]
        public async Task<IActionResult> GetOrders()
        {
            var result = await _getOrdersService.GetAllOrders();

            
            return Ok(result);
        }

        /// <summary>
        /// Retorna produções filtradas por e-mail.
        /// </summary>
        [HttpGet("GetProduction")]
        [ProducesResponseType(typeof(GetProductionResponseDTO), 200)]
        [ProducesResponseType(typeof(BadRequestObjectResult), 400)]
        public async Task<IActionResult> GetProduction([FromQuery] string email)
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
        [ProducesResponseType(typeof(SetProductionResponseDTO), 200)]
        [ProducesResponseType(typeof(SetProductionResponseDTO), 201)]
        [ProducesResponseType(typeof(BadRequestObjectResult), 400)]
        public async Task<IActionResult> SetProduction([FromBody] SetProductionRequestDTO request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _setProductionService.SetProductionAsync(request);

            if (result.Data == null)
                return StatusCode(500, new { message = "Erro interno no apontamento de produção." });

          
            return StatusCode(result.Data.Status, result.Data);
        }
    }
}
