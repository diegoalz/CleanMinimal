using ErrorOr;
using MediatR;

namespace CleanMinimal.Application.Features.Users.Register;

public record RegisterUserCommand(
    string Name,
    string LastName,
    string Email,
    string PhoneNumber,
    bool Active = true
): IRequest<ErrorOr<Guid>>;