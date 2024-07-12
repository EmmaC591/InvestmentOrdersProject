using InvestmentOrdersProject.Application.DTOs;
using InvestmentOrdersProject.Application.Interfaces;
using InvestmentOrdersProject.Infrastructure.AuthService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentOrdersProject.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthManagement _authManagement;

        public AuthService(IAuthManagement authManagement)
        {
            _authManagement = authManagement;
        }

        public async Task<AuthenticationResponseDto> AuthenticateAsync(AuthenticationRequestDto request)
        {
            var token = await _authManagement.AuthenticateAsync(request.Email, request.Password);

            if (string.IsNullOrWhiteSpace(token))
            {
                throw new ArgumentException("Token vacio");
            }

            return new AuthenticationResponseDto { Token = token };
        }
    }
}
