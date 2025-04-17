
namespace Application.Dtos.Auth;

public class RegistroDto: BaseUsernameDto
{
    public required string Nombre { get; set; }
    public required string Apellido { get; set; }
    public required string Password { get; set; }

}