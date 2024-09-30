using CleanMinimal.Domain.Common;

namespace CleanMinimal.Application.Contracts.Responses;

public record UserResponse(
    Guid Id,
    string Name,
    string LastName,
    string FullName,
    string Email,
    string PhoneNumber,
    bool Active
);