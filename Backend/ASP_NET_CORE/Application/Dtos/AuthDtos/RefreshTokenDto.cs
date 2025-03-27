namespace Application.Dtos.AuthDtos;

public class RefreshTokenDto
{
    public string RefreshToken { get; set; }
    public RefreshTokenDto(string refreshToken)
    {
        RefreshToken = refreshToken;
    }
}
