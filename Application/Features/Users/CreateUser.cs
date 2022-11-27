using System.Net;
using Aplication.Dtos;
using Aplication.Errors;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Persistence;

namespace Application.Features.Users;

public class CreateUser
{
    public class CreateUserCommand :IRequest<UserDto>
        {
            public string Email { get; set; }
            public string FullName { get; set; }
            public string Password { get; set; }
            public string PhoneNumber { get; set; }
            public string UserName { get; set; }
        }

        public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
        {
            public CreateUserCommandValidator()
            {
                RuleFor(x => x.UserName).NotEmpty();
                RuleFor(x => x.Email).NotEmpty().EmailAddress();
                RuleFor(x => x.Password).NotEmpty().NotNull();
                
            }
                
        }

        public  class CreateUserCommandHandler :IRequestHandler<CreateUser.CreateUserCommand,UserDto>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            private readonly UserManager<User> _manager;

            public CreateUserCommandHandler(DataContext context, IMapper mapper,UserManager<User> manager)
            {
                _context = context;
                _mapper = mapper;
                _manager = manager;
            }
            public async Task<UserDto> Handle(CreateUser.CreateUserCommand request, CancellationToken cancellationToken)
            {
                
                var user = new User()
                {
                    FullName = request.FullName,
                    Email = request.Email,
                    UserName = request.UserName,
                    PhoneNumber = request.PhoneNumber
                };

                var result = await _manager.CreateAsync(user, request.Password);
              
               if (result.Succeeded)
               {
                   return _mapper.Map<User, UserDto>(user);
                   
               }
               throw new RestException(HttpStatusCode.Conflict,result.Errors);
              
             
              
            }
        }
}