using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boundaries.Persistance.Models.Category;

[Table("Category")]
public sealed class Category
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int CategoryId { get; set; } 
    
    [Required]
    [MaxLength(100)]
    [MinLength(3)]
    public string Name { get; set; } = String.Empty;
    
    [MaxLength(500)]
    public string Description { get; set; } = String.Empty;
    
    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    [Required]
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}