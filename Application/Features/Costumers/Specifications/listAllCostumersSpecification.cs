using Application.Specification;
using Domain;

namespace Application.Features.Costumers.Specifications;

public class ListAllCostumersSpecification: BaseSpecification<Costumer>
{
    public ListAllCostumersSpecification()
    {
        AddInclude(x=>x.CostumerType);
    }
}