using Application.Features.Suppliers.Specifications.Suppliers;
using Application.Interfaces;
using Domain;
using MediatR;

namespace Application.Features.Suppliers;

public class ListAllSupplier
{
    public class ListAllSupplierQuery: IRequest<List<Supplier>>
    {

    }
    
    public class ListAllSupplierQueryHandler: IRequestHandler<ListAllSupplierQuery, List<Supplier>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ListAllSupplierQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<Supplier>> Handle(ListAllSupplierQuery request, CancellationToken cancellationToken)
        {
            var spec = new ListAllSupplierSpecifications();
            return await _unitOfWork.Repository<Supplier>().ListWithSpecAsync(spec);
        }
    }
}