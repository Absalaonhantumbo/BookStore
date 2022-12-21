using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class Book: BaseEntity
{
    public string ISBN { get; set; }
    public string Title { get; set; }
    public string SubjectMatter { get; set; }
    public int QuantityStock { get; set; }
    public int Balance { get; set; }
    public int Edition { get; set; }
    public int Page { get; set; }
    public string ImageBook { get; set; }
    public int PublishingCompanyId { get; set; }
    [ForeignKey("PublishingCompanyId")] public PublishingCompany PublishingCompany { get; set; }
    
    public int DeweyDecimalClassificationId { get; set; }
    [ForeignKey("DeweyDecimalClassificationId")] public DeweyDecimalClassification DeweyDecimalClassification { get; set; }
    
    public int SupplierId { get; set; }
    [ForeignKey("SupplierId")] public Supplier Supplier { get; set; }

}