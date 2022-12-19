using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class BookDocument: BaseEntity
{
    public int BookId { get; set; }
    [ForeignKey("BookId")] public Book Book { get; set; }
    
    public string Name { get; set; }
    public string Token { get; set; }
}
