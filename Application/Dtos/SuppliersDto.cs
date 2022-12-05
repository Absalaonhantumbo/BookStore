namespace Application.Dtos;

public class SuppliersDto
{
    public int Id { get; set; }
    public int CountryId { get; set; }
    public string Country { get; set; }
    public int SupplierTypeId { get; set; }
    public string SupplierType { get; set; }
    public int CompanyTypeId { get; set; }
    public string CompanyType { get; set; }
    public string Code { get; set; }
    public string LegalName { get; set; }
    public string TradeName { get; set; }
    public string Telephone { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    
    public DateTime CreatedAt { get; set; }
    

}