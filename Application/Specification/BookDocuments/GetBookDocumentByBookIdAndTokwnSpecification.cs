using Domain;

namespace Application.Specification.BookDocuments;

public class GetBookDocumentByBookIdAndTokwnSpecification: BaseSpecification<BookDocument>
{
    public GetBookDocumentByBookIdAndTokwnSpecification(int bookId, string token)
        : base(x => x.BookId == bookId && x.Token == token)
    {
        AddInclude(x => x.Book);
    }
}