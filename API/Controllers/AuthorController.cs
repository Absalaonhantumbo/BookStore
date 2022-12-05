using Application.Features.Authors;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class AuthorController: BaseApiController
{
    private readonly IMediator _mediator;

    public AuthorController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<Author>>> GetAllPublishingCompanies()
    {
        return Ok(await _mediator.Send(new ListAllAuthor.ListAllAuthorQuery()));
    }
    
    [HttpPost]
    public async Task<ActionResult<Author>> CreatePublishingCompany(CreateListAuthor.CreateAuthorCommand command)
    {
        return  await _mediator.Send(command);
    }
}