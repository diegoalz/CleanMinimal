using CleanMinimal.Application.Contracts.Responses;
using ErrorOr;
using MediatR;

namespace CleanMinimal.Application.Features.Users.GeteById;

public record GetByIdUserQuery(Guid Id) : IRequest<ErrorOr<UserResponse>>;