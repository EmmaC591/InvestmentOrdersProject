using FluentValidation;
using InvestmentOrdersProject.Application.Interfaces;
using InvestmentOrdersProject.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentOrdersProject.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ServiceCollectionExtensions).Assembly);

            services.AddScoped<IInvestmentOrderService, InvestmentOrderService>();
            services.AddScoped<IAuthService, AuthService>();


            return services;
        }
    }
}
