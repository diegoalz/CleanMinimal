using CleanMinimal.Application.Contracts.Responses;
using CleanMinimal.Domain.Models;
using ErrorOr;
using MediatR;

namespace CleanMinimal.Application.Features.Users.GetAll;

internal sealed class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, ErrorOr<IReadOnlyList<UserResponse>>>
{
    private readonly IUserRepository _userRepository;

    public GetAllUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async Task<ErrorOr<IReadOnlyList<UserResponse>>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken)
    {
        IReadOnlyList<User> users = await _userRepository.GetAll(query.Search ?? "");

        return users
            .Select(user => new UserResponse(
                user.Id.Value,
                user.Name,
                user.LastName,
                user.FullName,
                user.Email.Value,
                user.PhoneNumber.Value,
                user.Active,
                user.sales.Select(sale => new SaleResponse(
                    sale.Id.Value,
                    sale.Amount,
                    sale.Concept,
                    sale.FormattedSaleDateTime,
                    sale.UserId.Value,
                    sale.FormattedAmount
                )).ToList()
            )).ToList();
    }
}