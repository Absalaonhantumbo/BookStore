namespace Application.Dtos;

public class BookAuthorDto
{
    public string ISBN { get; set; }
    public string Title { get; set; }
    public string SubjectMatter { get; set; }
    public int QuantityStock { get; set; }
    public double Value { get; set; }
    public int Edition { get; set; }
    public int Page { get; set; }
    public int PublishingCompanyId { get; set; }
    public string PublishingCompany { get; set; }
    public int DeweyDecimalClassificationId { get; set; }
    public string DeweyDecimalClassification { get; set; }
    public int SupplierId { get; set; }
    public string Supplier { get; set; }
    
    public List<AuthorBooks> Authors { get; set; } = new List<AuthorBooks>();
}

public class AuthorBooks
{
    public int Id { get; set; }
}