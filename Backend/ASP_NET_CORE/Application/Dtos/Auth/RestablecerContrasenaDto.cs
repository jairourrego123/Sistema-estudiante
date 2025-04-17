

namespace Application.Dtos.Auth;

public class RestablecerContrasenaDto: BaseUsernameDto
{
    public required string Token { get; set; } 
    public required string NuevaContrasena { get; set; }
}