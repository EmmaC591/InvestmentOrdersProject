using InvestmentOrdersProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentOrdersProject.Infrastructure.Data.Configurations
{
    public class InvestmentOrderConfiguration : IEntityTypeConfiguration<InvestmentOrder>
    {
        public void Configure(EntityTypeBuilder<InvestmentOrder> builder)
        {
            builder.ToTable("InvestmentOrders");
            
            builder.HasKey(e => e.OrderId);

            builder.Property(io => io.Quantity).IsRequired();
            builder.Property(io => io.Price).IsRequired().HasColumnType("decimal(32,8)");
            builder.Property(io => io.Operation).IsRequired();
            builder.Property(io => io.TotalAmount).HasColumnType("decimal(32,8)");

            builder.HasOne(o => o.OrderStatus)
                .WithMany()
                .HasForeignKey(o => o.OrderStatusId);

            builder.HasOne(io => io.Asset)
                .WithMany()
                .HasForeignKey(io => io.AssetId);

            
        }
    }
}
