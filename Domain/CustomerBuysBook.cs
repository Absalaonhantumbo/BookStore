using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class CustomerBuysBook : BaseEntity
{
    public int BookId { get; set; }
    [ForeignKey("BookId")] public Book Book { get; set; }
    
    public int CostumerId { get; set; }
    [ForeignKey("CostumerId")] public Costumer Costumer { get; set; }

    public int Amount { get; set; }
    
    public double SaleValue { get; set; }
    
    public DateTime DateOfSale { get; set; } = DateTime.UtcNow;
}