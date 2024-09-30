using CleanMinimal.Application.Features.Users.Register;
using CleanMinimal.Domain;
using CleanMinimal.Domain.Common;
using CleanMinimal.Domain.Models;
using CleanMinimal.Domain.Primitives;
using CleanMinimal.Domain.ValueObjects;
using ErrorOr;
using MediatR;

namespace CleanMinimal.Application.Features.Users;

public sealed class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, ErrorOr<Guid>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    public RegisterUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<Guid>> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        if (PhoneNumber.Create(command.PhoneNumber) is not PhoneNumber phoneNumber)
        {
            // return Errors
            // throw new NotImplementedException();
            return Errors.User.PhoneNumberWithBadFormat;
        }

        if(Email.Create(command.Email) is not Email email)
        {
            return Errors.User.EmailWithBadFormat;
        }
        var user = new User(
            new BaseId(Guid.NewGuid()),
            command.Name,
            command.LastName,
            email,
            phoneNumber,
            command.Active
        );
        _userRepository.Create(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return user.Id.Value;
    }
}