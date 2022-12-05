using Application.Specification;
using Domain;

namespace Application.Features.Suppliers.Specifications.Suppliers;

public class ListAllSupplierSpecifications : BaseSpecification<Supplier>
{
    public ListAllSupplierSpecifications()
    {
        AddInclude(x => x.Country);
        AddInclude(x => x.CompanyType);
        AddInclude(x => x.SupplierType);
    }

    public ListAllSupplierSpecifications(int id) : base(x => x.Id == id)
    {
        AddInclude(x => x.Country);
        AddInclude(x => x.CompanyType);
        AddInclude(x => x.SupplierType);
    }
}