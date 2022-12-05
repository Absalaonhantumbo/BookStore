using Application.Interfaces;
using Domain;
using MediatR;

namespace Application.Features.SupplierTypes;

public class ListAllSupplierType
{
    public class ListAllSupplierTypeQuery: IRequest<List<SupplierType>>
    {

    }
    
    public class ListAllSupplierTypeQueryHandler: IRequestHandler<ListAllSupplierTypeQuery, List<SupplierType>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ListAllSupplierTypeQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<SupplierType>> Handle(ListAllSupplierTypeQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Repository<SupplierType>().ListAllAsync();
        }
    }
}