using Application.Interfaces;
using Domain;
using MediatR;

namespace Application.Features.CompanyTypes;

public class ListAllCompanyType
{
    public class ListAllCompanyTypeQuery: IRequest<List<CompanyType>>
    {

    }
    
    public class ListAllCompanyTypeQueryHandler: IRequestHandler<ListAllCompanyTypeQuery, List<CompanyType>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ListAllCompanyTypeQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<CompanyType>> Handle(ListAllCompanyTypeQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Repository<CompanyType>().ListAllAsync();
        }
    }
    
}