using CleanMinimal.Domain.Common;
using CleanMinimal.Domain.ValueObjects;

namespace CleanMinimal.Domain.Models;

public sealed class Sale : BaseAuditableModel
{
    public Sale(
        BaseId id,
        int amount,
        string concept,
        CustomDateTime saleDateTime,
        Guid userId
    ) : base(id)
    {
        Amount = amount;
        Concept = concept;
        SaleDateTime = saleDateTime;
        UserId = new BaseId(userId);
    }

    protected Sale(BaseId id): base(id)
    {

    }

    public int Amount { get; set; }
    public string Concept { get; set; }
    public CustomDateTime SaleDateTime { get; set; }
    public BaseId UserId { get; set; }
    public decimal FormattedAmount => (decimal)Amount / 100;
    public string FormattedSaleDateTime => SaleDateTime.GetFormatted("yyyy-MM-dd HH:mm:ss");
    public User user { get; set; }
}