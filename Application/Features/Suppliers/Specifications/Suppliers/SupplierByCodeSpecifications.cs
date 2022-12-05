using Application.Specification;
using Domain;

namespace Application.Features.Suppliers.Specifications.Suppliers;

public class SupplierByCodeSpecifications : BaseSpecification<Supplier>
{
    public SupplierByCodeSpecifications(string code) : base(x => x.Code == code)
    {
    }
}