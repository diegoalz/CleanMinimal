using CleanMinimal.Domain.Primitives;

namespace CleanMinimal.Domain.Common;

public abstract class BaseAuditableModel : AggregateRoot
{
    protected BaseAuditableModel(BaseId id)
    {
        Id = id;
    }

    public BaseId Id { get; private set; }
    public DateTime? CreatedDate { get; set; }
    public int? CreatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public int? UpdatedBy { get; set; }
}