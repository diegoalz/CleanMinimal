using CleanMinimal.Application.Features.Sales.Register;
using FluentValidation;

namespace CleanMinimal.Application.Features.Sales;

public class RegisterSaleCommandValidation : AbstractValidator<RegisterSaleCommand>
{
    public RegisterSaleCommandValidation()
    {
        RuleFor(r => r.Amount)
            .NotEmpty();
        
        RuleFor(r => r.Concept)
            .MaximumLength(120);

        RuleFor(r => r.SaleDateTime)
            .NotEmpty();
        
        RuleFor(r => r.UserId)
            .NotEmpty();
    }
}