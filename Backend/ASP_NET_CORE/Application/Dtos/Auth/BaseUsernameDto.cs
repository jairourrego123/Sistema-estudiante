
using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Auth;

public class BaseUsernameDto
{
    [EmailAddress]
    public required string  Email { get; set; }
}
