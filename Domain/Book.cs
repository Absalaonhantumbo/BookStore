using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class Book: BaseEntity
{
    public string ISBN { get; set; }
    public string SubjectMatter { get; set; }
    public string QuantityStock { get; set; }
    public int PublishingCompanyId { get; set; }
    [ForeignKey("PublishingCompanyId")] public PublishingCompany PublishingCompany { get; set; }

}