using ErrorOr;
using MediatR;

namespace CleanMinimal.Application.Features.Sales.Register;

public record RegisterSaleCommand(
    int Amount,
    string Concept,
    string SaleDateTime,
    Guid UserId
): IRequest<ErrorOr<Guid>>;