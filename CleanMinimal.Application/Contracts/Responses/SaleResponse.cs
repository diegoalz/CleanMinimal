namespace CleanMinimal.Application.Contracts.Responses;

public record SaleResponse(
    Guid Id,
    int Amount,
    string Concept,
    string SaleDateTime,
    Guid UserId,
    decimal FormattedAmount
);