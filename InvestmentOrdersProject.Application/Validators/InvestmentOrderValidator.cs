using FluentValidation;
using InvestmentOrdersProject.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentOrdersProject.Application.Validators
{
    public class InvestmentOrderValidator : AbstractValidator<InvestmentOrderCreateDto>
    {
        public InvestmentOrderValidator()
        {
            RuleFor(x => x.AccountId).GreaterThan(0);
            RuleFor(x => x.AssetId).GreaterThan(0);
            RuleFor(x => x.Quantity).GreaterThan(0);
            RuleFor(x => x.Price).GreaterThan(0);
            RuleFor(x => x.Operation).Must(op => op == 'C' || op == 'V')
                .WithMessage("Operation must be 'C' for Buy or 'V' for Sell.");
        }
    }
}
