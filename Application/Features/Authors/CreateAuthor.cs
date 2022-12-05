using System.Net;
using Aplication.Errors;
using Application.Interfaces;
using Domain;
using FluentValidation;
using MediatR;

namespace Application.Features.Authors;

public class CreateListAuthor
{
    public class CreateAuthorCommand: IRequest<Author>
    {
        public string FullName { get; set; }
        public string Address { get; set; }
    }
    
    public class CreateAuthorCommandValidators: AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidators()
        {
            RuleFor(x => x.Address).NotEmpty().NotNull();
            RuleFor(x => x.FullName).NotEmpty().NotNull();
        }
    }
    
    public class CreateAuthorCommandHandler: IRequestHandler<CreateAuthorCommand, Author>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateAuthorCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Author> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = new Author()
            {
                FullName = request.FullName,
                Address = request.Address
            };
            
            _unitOfWork.Repository<Author>().Add(author);

            var result = await _unitOfWork.Complete() <0;

            if (result)
            {
                throw new RestException(HttpStatusCode.BadRequest, "Occurred problems for save Author");
            }

            return author;
        }
    }
    
}