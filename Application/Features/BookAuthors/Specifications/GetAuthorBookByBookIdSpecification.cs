using Application.Specification;
using Domain;

namespace Application.Features.BookAuthors.Specifications;

public class GetBookByBookIdSpecification: BaseSpecification<AuthorBook>
{
    public GetBookByBookIdSpecification(int bookId): base(x=>x.Book.Id == bookId)
    {
        AddInclude(x=>x.Author);
        AddInclude(x=>x.Book);
        AddInclude(x => x.Book.Supplier);
        AddInclude(x => x.Book.PublishingCompany);
        AddInclude(x => x.Book.DeweyDecimalClassification);
    }
}