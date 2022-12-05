using Application.Specification;
using Domain;

namespace Application.Features.Suppliers.Specifications.Suppliers;

public class GetSupplierByIdSpecification:BaseSpecification<Supplier>
{
    public GetSupplierByIdSpecification(int id):base(x=>x.Id==id)
    {
        
    }
}