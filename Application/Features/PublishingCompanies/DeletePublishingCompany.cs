using System.Net;
using Aplication.Errors;
using Application.Interfaces;
using Domain;
using MediatR;

namespace Application.Features.PublishingCompanies;

public class DeletePublishingCompany
{
    public class DeletePublishingCompanyCommand: IRequest<PublishingCompany>
    {
        public int PublishingCompanyId { get; set; }
        
    }
    
    public class DeletePublishingCompanyCommandHandler: IRequestHandler<DeletePublishingCompanyCommand, PublishingCompany>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeletePublishingCompanyCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<PublishingCompany> Handle(DeletePublishingCompanyCommand request, CancellationToken cancellationToken)
        {
            var publishingCompany =
                await _unitOfWork.Repository<PublishingCompany>().GetByIdAsync(request.PublishingCompanyId);

            if (publishingCompany is null)
            {
                throw new RestException(HttpStatusCode.NotFound, "publishingCompany is Not Found");

            }
            _unitOfWork.Repository<PublishingCompany>().Delete(publishingCompany);

            var result = await _unitOfWork.Complete() > 0;
            if (result)
            {
                return publishingCompany;
            }

            throw new Exception("Occurred problems for delete publishingCompany");
        }
    }
}