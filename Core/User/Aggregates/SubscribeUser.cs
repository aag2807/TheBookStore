using System.ComponentModel.DataAnnotations;

namespace Core.User.Aggregates;

public sealed class SubscribeUser
{
    [Required(ErrorMessage = "Email is required")]
    [MaxLength(32,ErrorMessage = "Email is too long")]
    [MinLength(3, ErrorMessage = "Email is too short")]
    public string Email { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;
}