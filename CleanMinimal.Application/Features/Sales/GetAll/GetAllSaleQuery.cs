using CleanMinimal.Application.Contracts.Responses;
using ErrorOr;
using MediatR;

namespace CleanMinimal.Application.Features.Sales.GetAll;

public record GetAllSaleQuery(string? Search) : IRequest<ErrorOr<IReadOnlyList<SaleResponse>>>;