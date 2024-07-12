using InvestmentOrdersProject.Application.DTOs;
using InvestmentOrdersProject.Application.Models;
using InvestmentOrdersProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentOrdersProject.Application.Interfaces
{
    public interface IInvestmentOrderService
    {
        Task<IEnumerable<InvestmentOrderDto>> GetOrdersAsync();
        Task<Pagination<InvestmentOrderDto>> GetOrdersAsync(int pageNumber, int pageSize);
        Task<Result<InvestmentOrderDto>> GetOrderByIdAsync(int orderId);
        Task<Result<InvestmentOrderDto>> CreateOrderAsync(InvestmentOrderCreateDto orderDto);
        Task<Result<InvestmentOrderDto>> UpdateOrderAsync(InvestmentOrderUpdateDto orderDto);
        Task DeleteOrderAsync(int orderId);
    }
}
