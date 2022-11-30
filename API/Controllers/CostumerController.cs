using Application.Features.Costumers;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class CostumerController:BaseApiController
{
    private readonly IMediator _mediator;

    public CostumerController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<ActionResult<Costumer>> CreateCostumers(CreateCostumer.CreateCostumerCommand command)
    {
        return  await _mediator.Send(command);
    }
    
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<List<Costumer>>> GetAllCostumers()
    {
        return Ok(await _mediator.Send(new ListAllCostumer.ListAllCostumerQuery()));
    }
}