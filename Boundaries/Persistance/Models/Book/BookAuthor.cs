using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boundaries.Persistance.Models.Book;

[Table("BookAuthor")]
public sealed class BookAuthor
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int BookAuthorId { get; set; }

    public int BookId { get; set; }

    public int AuthorId { get; set; }
    
    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    [Required]
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}