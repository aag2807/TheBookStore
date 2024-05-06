using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boundaries.Persistance.Models.Book;

[Table("Book")]
public sealed class Book
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int BookId { get; set; }

    [Required]
    [MaxLength(100)]
    [MinLength(3)]
    public string Title { get; set; } = String.Empty;

    [Required]
    [MaxLength(100)]
    [MinLength(3)]
    public string ISBN { get; set; } = String.Empty;

    [Required] 
    public float Price { get; set; } = 0.00f;

    [Required] 
    public int StockQuantity { get; set; } = 0;
    
    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    [Required]
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    
    public Author.Author Author { get; set; } = new();
}