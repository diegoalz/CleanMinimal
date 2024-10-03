using CleanMinimal.Application.Contracts.Responses;
using CleanMinimal.Domain.Common;
using CleanMinimal.Domain.Models;
using ErrorOr;
using MediatR;

namespace CleanMinimal.Application.Features.Users.GeteById;

internal sealed class GetByIdUserQueryHandler : IRequestHandler<GetByIdUserQuery, ErrorOr<UserResponse>>
{
    private readonly IUserRepository _userRepository;
    public GetByIdUserQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async Task<ErrorOr<UserResponse>> Handle(GetByIdUserQuery query, CancellationToken cancellationToken)
    {
        if(await _userRepository.SelectById(new BaseId(query.Id)) is not User user)
        {
            return Error.NotFound("User.NotFound", "El usuario con este ID no se encuentra registrado");
        }
        return new UserResponse(
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
        );
    }
}