using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class CostumerBuyBook: BaseEntity
{
    public int BookId { get; set; }
    [ForeignKey("BookId")] public Book Book { get; set; }
    public int CostumerId { get; set; }
    [ForeignKey("CostumerId")] public Costumer Costumer { get; set; }
    public DateTime PurchaseDate { get; set; }
    public int QuantityBookBought { get; set; }
    public double SaleValue { get; set; }


}