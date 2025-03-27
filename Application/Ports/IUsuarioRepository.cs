using Application.Dtos.AuthDtos;

namespace Application.Ports;

public interface IUsuarioRepository
{
    Task CrearUsuarioAsync(RegistroDto registroDto);
    Task<UsuarioDto> ValidarCredenciales(LoginDto loginDto);
    Task<UsuarioDto?> ObtenerUsuarioPorEmailAsync(string email);

}
