using CleanMinimal.Application.Features.Sales.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanMinimal.API.Controllers;

[Route("sales")]
public class Sales : ApiController
{
    private readonly ISender _mediator;
    public Sales(ISender mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    // [HttpGet]
    // public async Task<IActionResult> GetAll(string query)
    // {
    //     var saleResult + await _mediator.Send(new GetAll)
    // }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RegisterSaleCommand command)
    {
        var createResult = await _mediator.Send(command);
        return createResult.Match(
            Id => Ok(Id),
            errors => Problem(errors)
        );
    }
}