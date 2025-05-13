namespace Application.Dtos.Auth;

public class LoginDto: BaseUsername
{
    public required string Password { get; set; }
}
