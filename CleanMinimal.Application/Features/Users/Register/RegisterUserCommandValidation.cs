using CleanMinimal.Application.Features.Users.Register;
using FluentValidation;

namespace CleanMinimal.Application.Features.Users;

public class RegisterUserCommandValidation : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidation()
    {
        RuleFor(r => r.Name)
            .NotEmpty()
            .MaximumLength(50);
        
        RuleFor(r => r.LastName)
            .NotEmpty()
            .MaximumLength(80)
            .WithName("Last Name");

        RuleFor(r => r.Email)
            .NotEmpty()
            .MaximumLength(255);
        
        RuleFor(r => r.PhoneNumber)
            .NotEmpty()
            .MaximumLength(13)
            .MinimumLength(12)
            .WithName("Phone Number");
        
        RuleFor(r => r.Active).NotEmpty();
    }
}