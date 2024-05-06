using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boundaries.Persistance.Models.Customer;

[Table("Customer")]
public sealed class Customer
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int CustomerId { get; set; }

    [Required]
    [MaxLength(100)]
    [MinLength(2)]
    public string FirstName { get; set; } = String.Empty;

    [Required]
    [MaxLength(100)]
    [MinLength(2)]
    public string LastName { get; set; } = String.Empty;

    [Required]
    [MaxLength(100)]
    [MinLength(10)]
    public string Email { get; set; } = String.Empty;

    [Required]
    [MaxLength(100)]
    [MinLength(10)]
    public string Phone { get; set; } = String.Empty;
    
    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    [Required]
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    
    public HashSet<Order.Order> Orders { get; set; } = new();
}