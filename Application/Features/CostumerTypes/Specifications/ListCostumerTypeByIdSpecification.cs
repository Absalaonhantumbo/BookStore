using Application.Specification;
using Domain;

namespace Application.Features.CostumerTypes.Specifications;

public class ListCostumerTypeByIdSpecification : BaseSpecification<CostumerType>
{
    public ListCostumerTypeByIdSpecification(int id): base(x=>x.Id == id)
    {
        
    }
}