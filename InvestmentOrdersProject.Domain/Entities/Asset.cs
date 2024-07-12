using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentOrdersProject.Domain.Entities
{
    public class Asset
    {
        public int Id { get; set; }
        public string Ticker { get; set; }
        public string Name { get; set; }
        public int AssetTypeId { get; set; }
        public AssetType AssetType { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
