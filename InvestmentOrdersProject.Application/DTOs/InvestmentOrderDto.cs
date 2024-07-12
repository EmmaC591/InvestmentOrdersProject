using InvestmentOrdersProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentOrdersProject.Application.DTOs
{
    public class InvestmentOrderDto
    {
        public int OrderId { get; set; }
        public int AccountId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public char Operation { get; set; } 
        public int OrderStatusId { get; set; }
        public int AssetId { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
