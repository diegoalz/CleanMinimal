using System.Transactions;
using MediatR;

namespace CleanMinimal.Domain.Primitives;

public record DomainEvent(Guid Id): INotification;