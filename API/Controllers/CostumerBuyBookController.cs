using Application.Features.Costumers;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using CustomerBuysBook = Application.Features.CustomerBuysBooks.CustomerBuysBook;

namespace API.Controllers;

public class CostumerBuyBookController : BaseApiController
{
    private readonly IMediator _mediator;

    public CostumerBuyBookController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<CostumerBuyBook>> CreateCostumerBuyBook(CustomerBuysBook.CustomerBuysBookCommand command)
    {
        return  await _mediator.Send(command);
    }
}