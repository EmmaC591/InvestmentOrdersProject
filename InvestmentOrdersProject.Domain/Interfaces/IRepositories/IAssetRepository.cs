using InvestmentOrdersProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentOrdersProject.Domain.Interfaces.IRepositories
{
    public interface IAssetRepository
    {
        Task<Asset> GetByIdAsync(int id);
    }
}
