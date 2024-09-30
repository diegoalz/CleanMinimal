using CleanMinimal.Application.Features.Users.Update;
using CleanMinimal.Domain;
using CleanMinimal.Domain.Models;
using CleanMinimal.Domain.Primitives;
using CleanMinimal.Domain.ValueObjects;
using ErrorOr;
using MediatR;

namespace CleanMinimal.Application.Features.Users.Update;

public sealed class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ErrorOr<Guid>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<Guid>> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        if (PhoneNumber.Create(command.PhoneNumber) is not PhoneNumber phoneNumber)
        {
            return Errors.User.PhoneNumberWithBadFormat;
        }

        if(Email.Create(command.Email) is not Email email)
        {
            return Errors.User.EmailWithBadFormat;
        }
        var user = User.Update(
            command.Id,
            command.Name,
            command.LastName,
            email,
            phoneNumber,
            command.Active
        );
        _userRepository.Update(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return user.Id.Value;
    }
}