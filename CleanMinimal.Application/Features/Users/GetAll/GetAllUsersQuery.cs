using CleanMinimal.Application.Contracts.Responses;
using ErrorOr;
using MediatR;

namespace CleanMinimal.Application.Features.Users.GetAll;

public record GetAllUsersQuery(string? Search) : IRequest<ErrorOr<IReadOnlyList<UserResponse>>>;