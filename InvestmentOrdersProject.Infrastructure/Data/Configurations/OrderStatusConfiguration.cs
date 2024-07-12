using InvestmentOrdersProject.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentOrdersProject.Infrastructure.Data.Configurations
{
    public class OrderStatusConfiguration : IEntityTypeConfiguration<OrderStatus>
    {
        public void Configure(EntityTypeBuilder<OrderStatus> builder)
        {
            builder.HasKey(os => os.StateId);

            builder.Property(os => os.Description)
                .IsRequired()
                .HasMaxLength(50);

            builder.ToTable("OrderStatuses");

            builder.HasData(
                new OrderStatus { StateId = 1, Description = "En proceso" },
                new OrderStatus { StateId = 2, Description = "Ejecutado" },
                new OrderStatus { StateId = 3, Description = "Cancelado" }
            );
        }
    }
}
