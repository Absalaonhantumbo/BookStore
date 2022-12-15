using Application.Specification;
using Domain;

namespace Application.Features.BookAuthors.Specifications;

public class GetListOfAuthorsById: BaseSpecification<Author>
{
    public GetListOfAuthorsById(List<int> authorId) : base(x => authorId.Contains(x.Id))
    {
    }
}