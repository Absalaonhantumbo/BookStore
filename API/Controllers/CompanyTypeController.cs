using Application.Features.CompanyTypes;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class CompanyTypeController: BaseApiController
{
    private readonly IMediator _mediator;

    public CompanyTypeController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<CompanyType>>> GetAllCompanyType()
    {
        return Ok(await _mediator.Send(new ListAllCompanyType.ListAllCompanyTypeQuery()));
    }
}