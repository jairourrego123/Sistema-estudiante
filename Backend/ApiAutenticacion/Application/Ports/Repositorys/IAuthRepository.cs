using Application.Dtos.Auth;

namespace Application.Ports.Repositorys;
public interface IAuthRepository
{
    Task RegistrarUsuarioAsync(RegistroDto dto);
    Task<ResponseJwtDto> LoginAsync(LoginDto dto);
    Task<ResponseJwtDto> RefreshAccessTokenAsync(RefreshTokenDto refreshToken);
    Task GenerarEnlaceRestablecimientoAsync(string email);
    Task RestablecerContrasenaAsync(RestablecerContrasenaDto restablecerContrasena);

}
