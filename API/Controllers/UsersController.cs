using Aplication.Dtos;
using Application.Features.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class UsersController:BaseApiController
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<UserDto>>> GetAllUser()
    {
        //ira buscar a informacao a base de dados
        return await _mediator.Send(new ListUsers.ListUsersQuery());
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<UserDto>> CreateUsers(CreateUser.CreateUserCommand command)
    {
        return  await _mediator.Send(command);//retorna um objecto com todos os posts
    }


}