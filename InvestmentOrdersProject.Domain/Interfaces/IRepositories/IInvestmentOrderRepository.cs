using InvestmentOrdersProject.Domain.Entities;
using InvestmentOrdersProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentOrdersProject.Domain.Interfaces.IRepositories
{
    public interface IInvestmentOrderRepository
    {
        Task<IEnumerable<InvestmentOrder>> GetOrdersAsync();
        Task<Pagination<InvestmentOrder>> GetOrdersAsync(int pageNumber, int pageSize);
        Task<InvestmentOrder> GetOrderByIdAsync(int orderId);
        Task AddOrderAsync(InvestmentOrder order);
        Task UpdateOrderAsync(InvestmentOrder order);
        Task DeleteOrderAsync(int orderId);
    }
}
