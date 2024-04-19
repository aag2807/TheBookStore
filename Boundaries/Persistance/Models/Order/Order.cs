using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boundaries.Persistance.Models.Order;

[Table("Order")]
public sealed class Order
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int OrderId { get; set; }

    [ForeignKey("Customer")]
    public int CustomerId { get; set; }

    public DateTime OrderDate { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal TotalPrice { get; set; }

    public Customer.Customer? Customer { get; set; }
    
    public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    
    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    [Required]
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}