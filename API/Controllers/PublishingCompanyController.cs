using Application.Features.PublishingCompanies;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class PublishingCompanyController: BaseApiController
{
    private readonly IMediator _mediator;

    public PublishingCompanyController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<List<PublishingCompany>>> GetAllPublishingCompanies()
    {
        return Ok(await _mediator.Send(new ListAllPublishingCompany.ListAllPublishingCompanyQuery()));
    }
    
    [HttpPost]
    public async Task<ActionResult<PublishingCompany>> CreatePublishingCompany(CreatePublishingCompany.CreatePublishingCompanyCommand command)
    {
        return  await _mediator.Send(command);
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<PublishingCompany>> DeletePublishingCompany(int id)
    {
        return await _mediator.Send(new DeletePublishingCompany.DeletePublishingCompanyCommand() { PublishingCompanyId = id });
    }
}