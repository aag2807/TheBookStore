using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boundaries.Persistance.Models.Author;

[Table("Author")]
public sealed class Author
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; set; } = 0;

    [Required]
    [MaxLength(100)]
    [MinLength(2)]
    public string FirstName { get; set; } = String.Empty;
    
    [Required]
    [MaxLength(100)]
    [MinLength(2)]
    public string LastName { get; set; } = String.Empty;
    
    [MaxLength(500)]
    public string Biography { get; set; } = String.Empty;
        
    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    [Required]
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}