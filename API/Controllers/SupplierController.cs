using Application.Dtos;
using Application.Features.Suppliers;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers;

public class SupplierController : BaseApiController
{
    private readonly IMediator _mediator;

    public SupplierController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<SuppliersDto>>> GetAllDepartments()
    {
        return Ok(await _mediator.Send(new ListAllSupplier.ListAllSupplierQuery()));
    }

    [HttpPost]
    public async Task<ActionResult<SuppliersDto>> CreateSuppliers(CreateSupplier.CreateSupplierCommand command)
    {
        return await _mediator.Send(command);
    }

    [HttpGet("{id}")]
    public async Task<SuppliersDto> GetSuppliersById(int id)
    {
        return await _mediator.Send(new GetSuppliersById.GetSuppliersByIdQuery() { SupplierId = id });
    }

    [HttpDelete("{id}")]
    public async Task<Supplier> DeleteDepartment(int id)
    {
        return await _mediator.Send(new DeleteSupplier.DeleteSupplierCommand() { SupplierId = id });
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<SuppliersDto>> UpdateDepartment(int id, UpdateSupplier.UpdateSupplierCommad command)
    {
        command.Id = id;
        return await _mediator.Send(command);
    }
}