using Application.Specification;
using Domain;

namespace Application.Features.Costumers.Specifications;

public class ListCostumerById : BaseSpecification<Costumer>
{
    public ListCostumerById(int id): base(x=>x.Id ==id)
    {
        AddInclude(x=>x.CostumerType);
    }
}