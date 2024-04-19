using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boundaries.Persistance.Models.User;

[Table("User")]
public sealed class User
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int UserId { get; set; }
    
    [Required]
    [MaxLength(100)]
    [MinLength(3)]
    public string Username { get; set; } = String.Empty;
    
    [Required]
    [MaxLength(100)]
    [MinLength(3)]
    public string Password { get; set; } = String.Empty;
    
    [Required]
    [MaxLength(100)]
    [MinLength(3)]
    public string Email { get; set; } = String.Empty;
    
    [Required]
    public bool IsAdmin { get; set; } = false;
    
    [Required]
    public bool IsBlocked { get; set; } = false;
    
    [Required]
    public bool IsDeleted { get; set; } = false;
    
    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    [Required]
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}