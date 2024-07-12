using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentOrdersProject.Infrastructure.AuthService
{
    public interface IAuthManagement
    {
        Task<string> AuthenticateAsync(string email, string password);
    }
}
