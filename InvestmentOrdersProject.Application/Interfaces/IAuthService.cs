using InvestmentOrdersProject.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentOrdersProject.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthenticationResponseDto> AuthenticateAsync(AuthenticationRequestDto request);
    }
}
