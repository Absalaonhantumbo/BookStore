using Application.Features.Countries;
using Application.Features.SupplierTypes;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class SupplierTypeController: BaseApiController
{
    private readonly IMediator _mediator;

    public SupplierTypeController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<SupplierType>>> GetAllSupplierType()
    {
        return Ok(await _mediator.Send(new ListAllSupplierType.ListAllSupplierTypeQuery()));
    }
    
}