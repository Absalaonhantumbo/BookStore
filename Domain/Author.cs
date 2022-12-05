using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class Author: BaseEntity
{
    public string FullName { get; set; }
    public string Address { get; set; }
   
}