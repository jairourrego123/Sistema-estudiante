namespace Application.Dtos.Auth;

public class LoginDto: BaseUsernameDto
{
    public required string Password { get; set; }
}
