using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ConsoleApp.Entities;

public class OrderEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "datetime2")]
    public DateTime OrderDate { get; set; }

    [Required]
    [Column(TypeName = "money")]
    public decimal TotalPrice { get; set; }


    public int CustomerId { get; set; }



    public virtual CustomerEntity Customer { get; set; } = null!;
    public virtual ICollection<DetailEntity> Details { get; set; } = null!;
}
