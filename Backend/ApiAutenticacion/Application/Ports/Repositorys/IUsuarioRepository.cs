using Application.Dtos.Auth;

namespace Application.Ports.Repositorys;

public interface IUsuarioRepository
{
    Task CrearUsuarioAsync(RegistroDto registroDto);
    Task<UsuarioDto> ValidarCredenciales(LoginDto loginDto);
    Task<UsuarioDto> ObtenerUsuarioPorEmailAsync(string email);
    Task RestablecerContrasena(RestablecerContrasenaDto restablecerContrasena);
    Task<string> GenerarTokenRestablecimientoContrasena(string email);

}
