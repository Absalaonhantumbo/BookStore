using Application.Specification;
using Domain;

namespace Application.Features.BookAuthors.Specifications;

public class GetAllBook: BaseSpecification<Book>
{
    public GetAllBook(int bookId): base(x=>x.Id == bookId)
    {
    }
}