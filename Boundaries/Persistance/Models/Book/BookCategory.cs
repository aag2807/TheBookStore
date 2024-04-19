using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boundaries.Persistance.Models.Book;

[Table("BookCategory")]
public sealed class BookCategory
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int BookCategoryId { get; set; }
    
    [Required]
    [ForeignKey("Book")]
    public int BookId { get; set; }
    
    [Required]
    [ForeignKey("Category")]
    public int CategoryId { get; set; }
    
    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    [Required]
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}