using System.Net;
using Aplication.Errors;
using Application.Interfaces;
using Domain;
using FluentValidation;
using MediatR;

namespace Application.Features.PublishingCompanies;

public class CreatePublishingCompany
{
    public class CreatePublishingCompanyCommand: IRequest<PublishingCompany>
    {
        public string Code { get; set; }
        public string Gerente { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
    
    public class CreatePublishingCompanyCommandValidators: AbstractValidator<CreatePublishingCompanyCommand>
    {
        public CreatePublishingCompanyCommandValidators()
        {
            RuleFor(x => x.Address).NotEmpty().NotNull();
            RuleFor(x => x.Code).NotEmpty().NotNull();
            RuleFor(x => x.Gerente).NotEmpty().NotNull();
            RuleFor(x => x.PhoneNumber).NotEmpty().NotNull();
        }
    }
    
    public class CreatePublishingCompanyCommandHandler: IRequestHandler<CreatePublishingCompanyCommand, PublishingCompany>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreatePublishingCompanyCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<PublishingCompany> Handle(CreatePublishingCompanyCommand request, CancellationToken cancellationToken)
        {
            var publishingCompany = new PublishingCompany()
            {
                Code = request.Code,
                Gerente = request.Gerente,
                Address = request.Address,
                PhoneNumber = request.PhoneNumber
            };
            _unitOfWork.Repository<PublishingCompany>().Add(publishingCompany);

            var result= await _unitOfWork.Complete() < 0;
            if (result)
            {
                throw new RestException(HttpStatusCode.BadRequest, "Erro to save publishing company");
            }

            return publishingCompany;
        }
    }
    
}