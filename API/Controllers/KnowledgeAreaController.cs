using Application.Features.knowledgeAreas;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class KnowledgeAreaController: BaseApiController
{
    private readonly IMediator _mediator;

    public KnowledgeAreaController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<knowledgeArea>>> GetAllPublishingCompanies()
    {
        return Ok(await _mediator.Send(new ListknowledgeArea.ListknowledgeAreaQuery()));
    }
    
    [HttpPost]
    public async Task<ActionResult<knowledgeArea>> CreatePublishingCompany(CreateknowledgeArea.CreateknowledgeAreaCommand command)
    {
        return  await _mediator.Send(command);
    }
}