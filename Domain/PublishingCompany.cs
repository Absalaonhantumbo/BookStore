namespace Domain;

public class PublishingCompany: BaseEntity
{
    public string Code { get; set; }
    public string Gerente { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
}