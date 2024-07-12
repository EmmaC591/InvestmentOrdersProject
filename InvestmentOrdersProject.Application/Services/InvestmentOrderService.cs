using AutoMapper;
using InvestmentOrdersProject.Application.DTOs;
using InvestmentOrdersProject.Application.Interfaces;
using InvestmentOrdersProject.Application.Models;
using InvestmentOrdersProject.Domain.Entities;
using InvestmentOrdersProject.Domain.Enums;
using InvestmentOrdersProject.Domain.Interfaces.IRepositories;
using InvestmentOrdersProject.Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentOrdersProject.Application.Services
{
    public class InvestmentOrderService : IInvestmentOrderService
    {
        private readonly IInvestmentOrderRepository _investmentOrderRepository;
        private readonly IAssetRepository _assetRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public InvestmentOrderService(
            IInvestmentOrderRepository investmentOrderRepository, 
            IAssetRepository assetRepository,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _investmentOrderRepository = investmentOrderRepository;
            _assetRepository = assetRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<InvestmentOrderDto>> GetOrdersAsync()
        {
            var orders = await _investmentOrderRepository.GetOrdersAsync();
            return _mapper.Map<IEnumerable<InvestmentOrderDto>>(orders);
        }

        public async Task<Pagination<InvestmentOrderDto>> GetOrdersAsync(int pageNumber, int pageSize)
        {
            var paginatedOrders = await _investmentOrderRepository.GetOrdersAsync(pageNumber, pageSize);
            var mappedOrders = _mapper.Map<List<InvestmentOrderDto>>(paginatedOrders.Items);

            return new Pagination<InvestmentOrderDto>(mappedOrders, paginatedOrders.TotalCount, paginatedOrders.PageNumber, paginatedOrders.PageSize);
        }

        public async Task<Result<InvestmentOrderDto>> GetOrderByIdAsync(int orderId)
        {
            var order = await _investmentOrderRepository.GetOrderByIdAsync(orderId);

            if (order == null)
            {
                return Result<InvestmentOrderDto>.Failure("Orden no encontrada.");
            }

            var orderDto = _mapper.Map<InvestmentOrderDto>(order);
            return Result<InvestmentOrderDto>.Success(orderDto);
        }

        public async Task<Result<InvestmentOrderDto>> CreateOrderAsync(InvestmentOrderCreateDto orderDto)
        {
            var order = _mapper.Map<InvestmentOrder>(orderDto);

            var asset = await _assetRepository.GetByIdAsync(order.AssetId);
            if (asset == null)
            {
                return Result<InvestmentOrderDto>.Failure("El activo no existe.");
            }

            // No se debe proporcionar el precio para el tipo de activo 'Accion'
            if (asset.AssetTypeId == (int)AssetTypeEnum.Accion && orderDto.Price > 0)
            {
                return Result<InvestmentOrderDto>.Failure("El precio no debe ser proporcionado para el tipo de activo 'Accion'.");
            }

            // Establecer el estado inicial
            order.OrderStatusId = 1; // "En proceso"

            
            // Calcular el Monto Total basado en el tipo de activo
            switch (asset.AssetTypeId)
            {
                case (int)AssetTypeEnum.FCI:
                    order.TotalAmount = asset.UnitPrice * order.Quantity;
                    break;
                case (int)AssetTypeEnum.Accion:
                    order.Price = asset.UnitPrice;
                    order.TotalAmount = CalculateTotalAmountWithCommissions(order.Price, order.Quantity, 0.006m);
                    break;
                case (int)AssetTypeEnum.Bono:
                    order.TotalAmount = CalculateTotalAmountWithCommissions(order.Price, order.Quantity, 0.002m);
                    break;
                default:
                    return Result<InvestmentOrderDto>.Failure("Tipo de activo desconocido.");
            }

            order.CreatedBy = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
            order.CreatedDate = DateTime.UtcNow;

            try
            {
                await _investmentOrderRepository.AddOrderAsync(order);
                var resultDto = _mapper.Map<InvestmentOrderDto>(order);
                return Result<InvestmentOrderDto>.Success(resultDto);
            }
            catch (Exception ex)
            {
                
                return Result<InvestmentOrderDto>.Failure($"Error al crear la orden: {ex.Message}");
            }
        }

        public async Task<Result<InvestmentOrderDto>> UpdateOrderAsync(InvestmentOrderUpdateDto orderDto)
        {
            var order = await _investmentOrderRepository.GetOrderByIdAsync(orderDto.OrderId);

            if (order == null)
            {
                return Result<InvestmentOrderDto>.Failure("Orden no encontrada.");
            }

            
            order.OrderStatusId = orderDto.OrderStatusId;
            order.ModifiedBy = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
            order.ModifiedDate = DateTime.UtcNow;

            try
            {
                await _investmentOrderRepository.UpdateOrderAsync(order);
                var resultDto = _mapper.Map<InvestmentOrderDto>(order);
                return Result<InvestmentOrderDto>.Success(resultDto);
            }
            catch (Exception ex)
            {
                return Result<InvestmentOrderDto>.Failure($"Error al actualizar la orden: {ex.Message}");
            }
        }

        public async Task DeleteOrderAsync(int orderId)
        {
            await _investmentOrderRepository.DeleteOrderAsync(orderId);
        }

        private decimal CalculateTotalAmountWithCommissions(decimal price, int quantity, decimal commissionRate)
        {
            var total = price * quantity;
            var commission = total * commissionRate;
            var tax = commission * 0.21m;
            return total + commission + tax;
        }
    }
}
