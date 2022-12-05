using Application.Interfaces;
using Domain;
using MediatR;

namespace Application.Features.PublishingCompanies;

public class ListAllPublishingCompany
{
    public class ListAllPublishingCompanyQuery: IRequest<List<PublishingCompany>>
    {

    }
    
    public class ListAllPublishingCompanyQueryHandler: IRequestHandler<ListAllPublishingCompanyQuery, List<PublishingCompany>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ListAllPublishingCompanyQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<PublishingCompany>> Handle(ListAllPublishingCompanyQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Repository<PublishingCompany>().ListAllAsync();
        }
    }
}