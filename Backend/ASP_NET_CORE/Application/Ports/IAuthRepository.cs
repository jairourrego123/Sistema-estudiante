using Application.Dtos.AuthDtos;
using Application.UseCases.Auth;

namespace Application.Ports;
public interface IAuthRepository
{
    Task RegistrarUsuarioAsync(RegistroDto dto);
    Task<ResponseJwtDto> LoginAsync(LoginDto dto);
    Task<ResponseJwtDto> RefreshAccessTokenAsync(RefreshTokenDto refreshToken);
    Task<string> EnviarEnlaceRestablecimientoContrasena(string email);

}
