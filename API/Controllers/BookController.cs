using Application.Dtos;
using Application.Features.BookAuthors;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class BookController: BaseApiController
{
    private readonly IMediator _mediator;

    public BookController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<ActionResult<BookAuthorDto>> CreateContractPayment(CreateBookAuthors.CreateBookAuthorsCommand command)
    {
        return await _mediator.Send(command);
    }
    
}