using CleanMinimal.Application.Features.Sales.Register;
using CleanMinimal.Domain;
using CleanMinimal.Domain.Common;
using CleanMinimal.Domain.Models;
using CleanMinimal.Domain.Primitives;
using CleanMinimal.Domain.ValueObjects;
using ErrorOr;
using MediatR;

namespace CleanMinimal.Application.Features.Sales;

public sealed class RegisterSaleCommandHandler: IRequestHandler<RegisterSaleCommand, ErrorOr<Guid>>
{
    private readonly ISaleRepository _saleRepository;

    private readonly IUnitOfWork _unitOfWork;

    public RegisterSaleCommandHandler(ISaleRepository saleRepository, IUnitOfWork unitOfWork)
    {
        _saleRepository = saleRepository ?? throw new ArgumentNullException(nameof(saleRepository));
        _unitOfWork = unitOfWork ?? throw new  ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<Guid>> Handle(RegisterSaleCommand command, CancellationToken cancellationToken)
    {
        if (CustomDateTime.Create(command.SaleDateTime) is not CustomDateTime saleDateTime)
        {
            // throw new NotImplementedException();
            return Errors.Sale.CustomDatetimeBadFormat;
        }

        var sale = new Sale(
            new BaseId(Guid.NewGuid()),
            command.Amount,
            command.Concept,
            saleDateTime,
            command.UserId
        );
        
        _saleRepository.Create(sale);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return sale.Id.Value;
    }
}