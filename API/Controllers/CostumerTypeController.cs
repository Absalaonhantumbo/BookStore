using Application.Features.CostumerTypes;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class CostumerTypeController: BaseApiController
{
    private readonly IMediator _mediator;

    public CostumerTypeController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<ActionResult<CostumerType>> CreateUsers(CreateCostumerType.CreateCostumerTypeCommand command)
    {
        return  await _mediator.Send(command);
    }
}