using InvestmentOrdersProject.Application.DTOs;
using InvestmentOrdersProject.Application.Interfaces;
using InvestmentOrdersProject.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InvestmentOrdersProject.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InvestmentOrderController : ControllerBase
    {
        private readonly IInvestmentOrderService _investmentOrderService;

        public InvestmentOrderController(IInvestmentOrderService investmentOrderService)
        {
            _investmentOrderService = investmentOrderService;
        }

        /// <summary>
        /// Obtiene todas las órdenes de inversión.
        /// </summary>
        /// <returns>Lista de órdenes de inversión.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<InvestmentOrderDto>), 200)]
        public async Task<ActionResult<IEnumerable<InvestmentOrderDto>>> GetOrders()
        {
            var orders = await _investmentOrderService.GetOrdersAsync();
            return Ok(orders);
        }

        /// <summary>
        /// Obtiene órdenes de inversión paginadas.
        /// </summary>
        /// <param name="pageNumber">Número de página.</param>
        /// <param name="pageSize">Tamaño de la página.</param>
        /// <returns>Lista paginada de órdenes de inversión.</returns>
        [HttpGet("page/{pageNumber}/pageSize/{pageSize}")]
        [ProducesResponseType(typeof(Pagination<InvestmentOrderDto>), 200)]
        public async Task<ActionResult<Pagination<InvestmentOrderDto>>> GetOrders(int pageNumber, int pageSize)
        {
            var pagedOrders = await _investmentOrderService.GetOrdersAsync(pageNumber, pageSize);
            return Ok(pagedOrders);
        }

        /// <summary>
        /// Obtiene una orden de inversión por su ID.
        /// </summary>
        /// <param name="orderId">ID de la orden de inversión.</param>
        /// <returns>Orden de inversión encontrada.</returns>
        [HttpGet("{orderId}")]
        [ProducesResponseType(typeof(InvestmentOrderDto), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetOrderById(int orderId)
        {
            var result = await _investmentOrderService.GetOrderByIdAsync(orderId);

            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Value);
        }

        /// <summary>
        /// Crea una nueva orden de inversión.
        /// </summary>
        /// <param name="orderDto">Datos de la orden de inversión a crear.</param>
        /// <returns>Resultado de la operación de creación.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(InvestmentOrderDto), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateOrder([FromBody] InvestmentOrderCreateDto orderDto)
        {
            var result = await _investmentOrderService.CreateOrderAsync(orderDto);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        /// <summary>
        /// Actualiza una orden de inversión existente.
        /// </summary>
        /// <param name="orderDto">Datos actualizados de la orden de inversión.</param>
        /// <returns>Resultado de la operación de actualización.</returns>
        [HttpPut]
        [ProducesResponseType(typeof(InvestmentOrderDto), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdateOrder([FromBody] InvestmentOrderUpdateDto orderDto)
        {
            var result = await _investmentOrderService.UpdateOrderAsync(orderDto);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        /// <summary>
        /// Elimina una orden de inversión por su ID.
        /// </summary>
        /// <param name="id">ID de la orden de inversión a eliminar.</param>
        /// <returns>Resultado de la operación de eliminación.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            await _investmentOrderService.DeleteOrderAsync(id);
            return NoContent();
        }
    }
}
