using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class Supplier: BaseEntity
{
    public int CountryId { get; set; }
    [ForeignKey("CountryId")] public Country Country { get; set; }

    public int SupplierTypeId { get; set; }
    [ForeignKey("SupplierTypeId")] public SupplierType SupplierType { get; set; }

    public int CompanyTypeId { get; set; }
    [ForeignKey("CompanyTypeId")] public CompanyType CompanyType { get; set; }

    public string Code { get; set; }
    public string LegalName { get; set; }
    public string TradeName { get; set; }
    public string Telephone { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
   
}