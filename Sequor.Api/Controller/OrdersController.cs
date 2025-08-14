using Microsoft.AspNetCore.Mvc;
using Sequor.Application.DTOs;
using Sequor.Application.IService;

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
        /// <returns>Lista de ordens</returns>
        [HttpGet("GetOrders")]
        [ProducesResponseType(typeof(GetOrdersResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetOrdersResponseDTO>> GetOrders()
        {
            var result = await _getOrdersService.GetAllOrders();
            return Ok(result);
        }

        /// <summary>
        /// Retorna produções filtradas por e-mail.
        /// </summary>
        /// <param name="email">Email do usuário</param>
        /// <returns>Lista de produções do usuário</returns>
        [HttpGet("GetProduction")]
        [ProducesResponseType(typeof(GetProductionResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetProduction([FromQuery] string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return BadRequest(new { message = "Email é obrigatório" });

            var result = await _getProductionByEmailService.Execute(email);

            // Sempre retorna lista (mesmo vazia)
            return Ok(result);
        }

        /// <summary>
        /// Registra uma produção seguindo as regras de negócio.
        /// </summary>
        /// <param name="request">Dados do apontamento de produção</param>
        /// <returns>Resultado do apontamento com status, tipo e descrição</returns>
        [HttpPost("SetProduction")]
        [ProducesResponseType(typeof(SetProductionResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SetProductionResponseDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BadRequestObjectResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SetProduction([FromBody] SetProductionRequestDTO request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _setProductionService.SetProductionAsync(request);
            return StatusCode(response.Status, response);
        }
    }
}
