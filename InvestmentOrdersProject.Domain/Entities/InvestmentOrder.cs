using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentOrdersProject.Domain.Entities
{
    public class InvestmentOrder : AuditableEntity
    {
        public int OrderId { get; set; }
        public int AccountId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public char Operation { get; set; }
        public int OrderStatusId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public int AssetId { get; set; }
        public Asset Asset { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
