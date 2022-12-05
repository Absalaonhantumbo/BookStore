using Application.Features.Countries;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class CountyController: BaseApiController
{
    private readonly IMediator _mediator;

    public CountyController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<Country>>> GetAllCountry()
    {
        return Ok(await _mediator.Send(new ListAllCountry.ListAllCountryQuery()));
    }

}