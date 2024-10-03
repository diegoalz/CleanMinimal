using CleanMinimal.Application.Features.Users.GetAll;
using CleanMinimal.Application.Features.Users.GeteById;
using CleanMinimal.Application.Features.Users.Register;
using CleanMinimal.Application.Features.Users.Update;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanMinimal.API.Controllers;

[Route("users")]
public class Users : ApiController
{
    private readonly ISender _mediator;
    public Users(ISender mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    [HttpGet]
    public async Task<IActionResult> GetAll(string? query)
    {
        var usersResult = await _mediator.Send(new GetAllUsersQuery(query));

        return usersResult.Match(
            users => Ok(users),
            errors => Problem(errors)
        );
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var userResult = await _mediator.Send(new GetByIdUserQuery(id));
        return userResult.Match(
            user => Ok(user),
            errors => Problem(errors)
        );
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RegisterUserCommand command)
    {
        var createResult = await _mediator.Send(command);
        return createResult.Match(
            Id => Ok(Id),
            errors => Problem(errors)
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserCommand command)
    {
        if(command.Id != id)
        {
            List<Error> errors = new()
            {
                Error.Validation("User.UpdateInvalid", "The request Id does not match with the url Id.")
            };
            return Problem(errors);
        }
        var updateResult = await _mediator.Send(command);

        return updateResult.Match(
            userId => NoContent(),
            errors => Problem(errors)
        );
    }
}