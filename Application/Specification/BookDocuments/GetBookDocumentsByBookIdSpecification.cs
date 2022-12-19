using Domain;

namespace Application.Specification.BookDocuments;

public class GetBookDocumentsByBookIdSpecification: BaseSpecification<BookDocument>
{
    public GetBookDocumentsByBookIdSpecification(int bookId): base(x=>x.BookId==bookId)
    {
        AddInclude(x=>x.Book);
        
    }
}