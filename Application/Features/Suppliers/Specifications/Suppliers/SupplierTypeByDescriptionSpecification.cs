using Application.Specification;
using Domain;

namespace Application.Features.Suppliers.Specifications.Suppliers;

public class SupplierTypeByDescriptionSpecification : BaseSpecification<SupplierType>
{
    public SupplierTypeByDescriptionSpecification(string description) : base(x => x.Description == description)
    {
    }
}