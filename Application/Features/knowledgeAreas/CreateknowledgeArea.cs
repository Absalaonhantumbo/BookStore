using System.Net;
using Aplication.Errors;
using Application.Interfaces;
using Domain;
using FluentValidation;
using MediatR;

namespace Application.Features.knowledgeAreas;

public class CreateknowledgeArea
{
    public class CreateknowledgeAreaCommand: IRequest<knowledgeArea>
    {
        public string Description { get; set; }
        
    }
    
    public class CreateknowledgeAreaCommandValidators: AbstractValidator<CreateknowledgeAreaCommand>
    {
        public CreateknowledgeAreaCommandValidators()
        {
            RuleFor(x => x.Description).NotEmpty().NotNull();
        }
    }
    
    public class CreateknowledgeAreaCommandHandler: IRequestHandler<CreateknowledgeAreaCommand, knowledgeArea>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateknowledgeAreaCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<knowledgeArea> Handle(CreateknowledgeAreaCommand request, CancellationToken cancellationToken)
        {
            var knowledgeArea = new knowledgeArea()
            {
                Description = request.Description,
                
            };
            
            _unitOfWork.Repository<knowledgeArea>().Add(knowledgeArea);

            var result = await _unitOfWork.Complete() <0;

            if (result)
            {
                throw new RestException(HttpStatusCode.BadRequest, "Occurred problems for save knowledgeArea");
            }

            return knowledgeArea;
        }
    }
}