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
    public class AssetTypeConfiguration : IEntityTypeConfiguration<AssetType>
    {
        public void Configure(EntityTypeBuilder<AssetType> builder)
        {
            builder.HasKey(at => at.Id);

            builder.Property(at => at.Description)
                .IsRequired()
                .HasMaxLength(50);

            builder.ToTable("AssetTypes");

            builder.HasData(
                new AssetType { Id = 1, Description = "Acción" },
                new AssetType { Id = 2, Description = "Bono" },
                new AssetType { Id = 3, Description = "FCI" }
            );
        }
    }
}
