using InvestmentOrdersProject.Domain.Entities;
using InvestmentOrdersProject.Domain.Interfaces.IRepositories;
using InvestmentOrdersProject.Domain.Models;
using InvestmentOrdersProject.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentOrdersProject.Infrastructure.Repositories
{
    public class InvestmentOrderRepository : IInvestmentOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public InvestmentOrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<InvestmentOrder>> GetOrdersAsync()
        {
            return await _context.InvestmentOrders.ToListAsync();
        }

        public async Task<Pagination<InvestmentOrder>> GetOrdersAsync(int pageNumber, int pageSize)
        {
            var count = await _context.InvestmentOrders.CountAsync();
            var items = await _context.InvestmentOrders
                                      .Skip((pageNumber - 1) * pageSize)
                                      .Take(pageSize)
                                      .ToListAsync();

            return new Pagination<InvestmentOrder>(items, count, pageNumber, pageSize);
        }

        public async Task<InvestmentOrder> GetOrderByIdAsync(int orderId)
        {
            return await _context.InvestmentOrders.FindAsync(orderId);
        }

        public async Task AddOrderAsync(InvestmentOrder order)
        {
            _context.InvestmentOrders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderAsync(InvestmentOrder order)
        {
            _context.Entry(order).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(int orderId)
        {
            var order = await _context.InvestmentOrders.FindAsync(orderId);
            if (order != null)
            {
                _context.InvestmentOrders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }
    }
}
