using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public string? CreatedByUserId { get; set; }
    [ForeignKey("CreatedByUserId")] public virtual User? CreatedByUser { get; set; }

}