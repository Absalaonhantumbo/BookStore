using Application.Features.DeweyDecimalClassifications;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class DeweyDecimalClassificationController: BaseApiController
{
    private readonly IMediator _mediator;

    public DeweyDecimalClassificationController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<DeweyDecimalClassification>>> GetAllCountry()
    {
        return Ok(await _mediator.Send(new ListAllDeweyDecimalClassification.ListAllDeweyDecimalClassificationQuery()));
    }
}