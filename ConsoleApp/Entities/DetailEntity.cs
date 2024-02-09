using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace ConsoleApp.Entities;

public class DetailEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int Quantity { get; set; }

    [Required]
    [Column(TypeName = "money")]
    public decimal Subtotal { get; set; }

    [Required]
    [ForeignKey(nameof(OrderEntity))]
    public int OrderId { get; set; }

    [Required]
    [ForeignKey(nameof(ProductEntity))]
    public int ProductId { get; set; }



    public virtual OrderEntity Order { get; set; } = null!;
    public virtual ProductEntity Product { get; set; } = null!;
}
