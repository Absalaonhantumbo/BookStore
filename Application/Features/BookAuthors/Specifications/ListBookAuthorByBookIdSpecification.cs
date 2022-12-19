using Application.Specification;
using Domain;

namespace Application.Features.BookAuthors.Specifications;

public class ListBookAuthorByBookIdSpecification: BaseSpecification<AuthorBook>
{
    public ListBookAuthorByBookIdSpecification(int bookId): base(x=>x.Book.Id == bookId)
    {
        
    }
}