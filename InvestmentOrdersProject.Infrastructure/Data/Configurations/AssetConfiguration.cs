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
    public class AssetConfiguration : IEntityTypeConfiguration<Asset>
    {
        public void Configure(EntityTypeBuilder<Asset> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Ticker)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.AssetTypeId)
                .IsRequired();

            builder.Property(a => a.UnitPrice)
                .HasColumnType("decimal(32,8)")
                .IsRequired();

            builder.ToTable("Assets");


            builder.HasData(
                new Asset { Id = 1, Ticker = "AAPL", Name = "Apple", AssetTypeId = 1, UnitPrice = 177.97m },
                new Asset { Id = 2, Ticker = "GOOGL", Name = "Alphabet Inc", AssetTypeId = 1, UnitPrice = 138.21m },
                new Asset { Id = 3, Ticker = "MSFT", Name = "Microsoft", AssetTypeId = 1, UnitPrice = 329.04m },
                new Asset { Id = 4, Ticker = "KO", Name = "Coca Cola", AssetTypeId = 1, UnitPrice = 58.3m },
                new Asset { Id = 5, Ticker = "WMT", Name = "Walmart", AssetTypeId = 1, UnitPrice = 163.42m },
                new Asset { Id = 6, Ticker = "AL30", Name = "BONOS ARGENTINA USD 2030 L.A", AssetTypeId = 2, UnitPrice = 307.4m },
                new Asset { Id = 7, Ticker = "GD30", Name = "Bonos Globales Argentina USD Step Up 2030", AssetTypeId = 2, UnitPrice = 336.1m },
                new Asset { Id = 8, Ticker = "Delta.Pesos", Name = "Delta Pesos Clase A", AssetTypeId = 3, UnitPrice = 0.0181m },
                new Asset { Id = 9, Ticker = "Fima.Premium", Name = "Fima Premium Clase A", AssetTypeId = 3, UnitPrice = 0.0317m }
            );
        }
    }
}
