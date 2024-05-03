using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Core.Utils;

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

    [Required] 
    public bool IsSubscribedToNewsLetter { get; set; } = false;
    
    [AllowNull]
    public DateTime? SubscriptionDate { get; set; } = null;
    
    [AllowNull]
    public DateTime? UnsubscriptionDate { get; set; } = null;

    public Core.User.User ToCoreEntity()
    {
        Core.User.User coreEntity = new Core.User.User();
        ObjectUtils.Assign(coreEntity, this);
        
        return coreEntity;
    }
    
    public static User FromCoreEntity(Core.User.User coreEntity)
    {
        User dbEntity = new User();
        ObjectUtils.Assign(dbEntity, coreEntity);
        
        return dbEntity;
    }
}