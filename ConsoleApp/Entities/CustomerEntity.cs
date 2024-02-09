using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ConsoleApp.Entities;

public class CustomerEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "varchar(200)")]
    public string Email { get; set; } = null!;

    [Required]
    [Column(TypeName = "nvarchar(max)")]
    public string Password { get; set; } = null!; 


    public virtual CustomerProfileEntity CustomerProfile { get; set; } = null!;
    public virtual ICollection<OrderEntity> Orders { get; set; } = null!;
}
