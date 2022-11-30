using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class Costumer : BaseEntity
{
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string? CPF { get; set; }
    public string? CNPJ { get; set; }
    public int CostumerTypeId { get; set; }
    [ForeignKey("CostumerTypeId")] public CostumerType CostumerType { get; set; }

}