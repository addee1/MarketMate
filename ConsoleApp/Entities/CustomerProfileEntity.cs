using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace ConsoleApp.Entities;

public class CustomerProfileEntity
{
    [Key]
    [ForeignKey(nameof(CustomerEntity))]
    public int CustomerId { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(30)")]
    public string FirstName { get; set; } = null!;

    [Required]
    [Column(TypeName = "nvarchar(30)")]
    public string LastName { get; set; } = null!;

    [Column(TypeName = "varchar(10)")]
    public string? PhoneNumber { get; set; }


    public virtual CustomerEntity Customer { get; set; } = null!;
}
