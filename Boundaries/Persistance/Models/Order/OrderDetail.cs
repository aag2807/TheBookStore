using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boundaries.Persistance.Models.Order;

[Table("OrderDetail")]
public sealed class OrderDetail
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int OrderDetailId { get; set; }
    
    [Required]
    [ForeignKey("Order")]
    public int OrderId { get; set; }
    
    [Required]
    [ForeignKey("Book")]
    public int BookId { get; set; }
    
    [Required]
    [Column(TypeName = "decimal(10, 2)")]
    public int Quantity { get; set; }
    
    [Required]
    [Column(TypeName = "decimal(10, 2)")]
    public decimal Price { get; set; }
    
    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    [Required]
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}