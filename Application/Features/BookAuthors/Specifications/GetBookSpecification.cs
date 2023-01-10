using Application.Specification;
using Domain;

namespace Application.Features.BookAuthors.Specifications;

public class GetBookSpecification: BaseSpecification<Book>
{
    public GetBookSpecification()
    {
        AddInclude(x=>x.PublishingCompany);
        AddInclude(x=>x.Supplier);
        AddInclude(x=>x.DeweyDecimalClassification);
    }
}