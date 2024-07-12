using InvestmentOrdersProject.Domain.Entities;
using InvestmentOrdersProject.Infrastructure.Data.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentOrdersProject.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<InvestmentOrder> InvestmentOrders { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<AssetType> AssetTypes { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new Configurations.InvestmentOrderConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.AssetConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.AssetTypeConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.OrderStatusConfiguration());

        }
    }
}
