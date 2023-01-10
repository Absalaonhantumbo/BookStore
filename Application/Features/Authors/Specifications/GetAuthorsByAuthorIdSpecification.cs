using Application.Specification;
using Domain;

namespace Application.Features.Authors.Specifications;

public class GetAuthorsByAuthorIdSpecification: BaseSpecification<Author>
{
    public GetAuthorsByAuthorIdSpecification(int authorId): base(x=>x.Id == authorId)
    {
        
    }
}