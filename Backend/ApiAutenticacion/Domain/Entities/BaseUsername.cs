
using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Auth;

public class BaseUsername
{
    [EmailAddress(ErrorMessage = "El campo {0} no cuenta con un formato correcto")]

    public required string  Email { get; set; }
}
