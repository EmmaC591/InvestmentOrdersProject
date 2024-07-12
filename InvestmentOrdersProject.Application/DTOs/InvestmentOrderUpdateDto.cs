using InvestmentOrdersProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentOrdersProject.Application.DTOs
{
    public class InvestmentOrderUpdateDto
    {
        public int OrderId { get; set; }
        public int OrderStatusId { get; set; }
    }
}
