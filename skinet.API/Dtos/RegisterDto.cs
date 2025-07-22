using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace skinet.API.Dtos;

public class RegisterDto
{
    [Required]
    public string DisplayName { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [RegularExpression("(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*()_+}{\":;'?/>.<,])(?!.*\\s).*$",ErrorMessage = "Password must be at least 6 characters long, contain at least one digit, one uppercase and one lowercase letter, one special character and no whitespace.")]
    public string Password { get; set; }
}