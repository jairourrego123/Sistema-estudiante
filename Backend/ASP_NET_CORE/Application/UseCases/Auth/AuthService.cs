using Application.Dtos.AuthDtos;
using Application.Ports;
using System.Security.Claims;

namespace Application.UseCases.Auth;

public class AuthService : IAuthRepository
{
    private readonly IJwtTokenRepository _jwtTokenRepository;
    private readonly IUsuarioRepository _usuarioRepository;

    public AuthService(IJwtTokenRepository jwtTokenRepository, IUsuarioRepository usuarioRepository)
    {
        _jwtTokenRepository = jwtTokenRepository;
        _usuarioRepository = usuarioRepository;
    }
    public async Task RegistrarUsuarioAsync(RegistroDto registroDto)
    {
        UsuarioDto? usuarioExistente = await _usuarioRepository.ObtenerUsuarioPorEmailAsync(registroDto.Email);
        if (usuarioExistente != null) throw new Exception("El usuario ya se encuentra registrado");
        await _usuarioRepository.CrearUsuarioAsync(registroDto);

     }

    public async Task<ResponseJwtDto> LoginAsync(LoginDto loginDto)
    {
        UsuarioDto usuario = await _usuarioRepository.ValidarCredenciales(loginDto)!;
        return _jwtTokenRepository.GenerarTokensAccesso(usuario);

    }

    public async Task<ResponseJwtDto> RefreshAccessTokenAsync(RefreshTokenDto refreshToken)
    {
        string emailUsuario = _jwtTokenRepository.ValidarRefreshToken(refreshToken.RefreshToken)!;
        if (emailUsuario == null)
        {
            throw new UnauthorizedAccessException("Refresh token inválido o expirado.");
        }

        UsuarioDto usuario = await _usuarioRepository.ObtenerUsuarioPorEmailAsync(emailUsuario) ??
                             throw new UnauthorizedAccessException("Usuario no encontrado.");

        return _jwtTokenRepository.GenerarTokensAccesso(usuario,false);
    }

    public async Task<string> EnviarEnlaceRestablecimientoContrasena(string email)
    {
        UsuarioDto? usuario = await _usuarioRepository.ObtenerUsuarioPorEmailAsync(email);
        if (usuario == null) return null!;

        return _jwtTokenRepository.GenerarTokenRestablecimientoContrasena(email)!;

    }
}
