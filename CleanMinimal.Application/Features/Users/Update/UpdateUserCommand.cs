using ErrorOr;
using MediatR;

namespace CleanMinimal.Application.Features.Users.Update;

public record UpdateUserCommand(
    Guid Id,
    string Name,
    string LastName,
    string Email,
    string PhoneNumber,
    bool Active = true
): IRequest<ErrorOr<Guid>>;