using CleanMinimal.Application.Contracts.Responses;
using CleanMinimal.Domain.Models;
using ErrorOr;
using MediatR;

namespace CleanMinimal.Application.Features.Sales.GetAll;

public sealed class GetAllQueryHandler : IRequestHandler<GetAllSaleQuery, ErrorOr<IReadOnlyList<SaleResponse>>>
{
    private readonly ISaleRepository _saleRepository;
    public GetAllQueryHandler(ISaleRepository saleRepository)
    {
        _saleRepository = saleRepository ?? throw new ArgumentNullException(nameof(saleRepository));
    }

    public async Task<ErrorOr<IReadOnlyList<SaleResponse>>> Handle(GetAllSaleQuery query, CancellationToken cancellationToken)
    {
        IReadOnlyList<Sale> sales = await _saleRepository.GetAll(query.Search ?? "");

        return sales
            .Select(sale => new SaleResponse(
                sale.Id.Value,
                sale.Amount,
                sale.Concept,
                sale.FormattedSaleDateTime,
                sale.UserId.Value,
                sale.FormattedAmount
            )).ToList();
    }
}