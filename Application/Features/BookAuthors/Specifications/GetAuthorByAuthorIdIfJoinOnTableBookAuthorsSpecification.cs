using Application.Specification;
using Domain;

namespace Application.Features.BookAuthors.Specifications;

public class GetAuthorByAuthorIdIfJoinOnTableBookAuthorsSpecification: BaseSpecification<AuthorBook>
{
    public GetAuthorByAuthorIdIfJoinOnTableBookAuthorsSpecification(int authorId): base(x=>x.AuthorId == authorId)
    {
        
    }
}