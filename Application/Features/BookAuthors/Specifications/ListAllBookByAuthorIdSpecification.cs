using Application.Specification;
using Domain;

namespace Application.Features.BookAuthors.Specifications;

public class ListAllBookByAuthorIdSpecification: BaseSpecification<AuthorBook>
{
    public ListAllBookByAuthorIdSpecification(int authorId): base(x=>x.Author.Id == authorId)
    {
        AddInclude(x=>x.Author);
        AddInclude(x=>x.Book);
        AddInclude(x => x.Book.Supplier);
        AddInclude(x => x.Book.PublishingCompany);
        AddInclude(x => x.Book.DeweyDecimalClassification);
        
    }
}