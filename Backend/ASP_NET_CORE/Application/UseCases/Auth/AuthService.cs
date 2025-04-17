using Application.Dtos.Auth;
using Application.Dtos.Notificacion;
using Application.Ports.Repositorys;
using Application.Ports.Services;
using System.Web;

namespace Application.UseCases.Auth;

public class AuthService : IAuthRepository
{
    private readonly IJwtTokenRepository _jwtTokenRepository;
    private readonly IUsuarioRepository _usuarioRepository; 
    private readonly INotificacionService _notificacionRepository;

    public AuthService(IJwtTokenRepository jwtTokenRepository, IUsuarioRepository usuarioRepository,INotificacionService notificacionRepository)
    {
        _jwtTokenRepository = jwtTokenRepository;
        _usuarioRepository = usuarioRepository;
        _notificacionRepository = notificacionRepository;
    }
    public async Task RegistrarUsuarioAsync(RegistroDto registroDto)
    {
        await _usuarioRepository.CrearUsuarioAsync(registroDto);

    }

    public async Task<ResponseJwtDto> LoginAsync(LoginDto loginDto)
    {
        UsuarioDto usuario = await _usuarioRepository.ValidarCredenciales(loginDto)!;
        return _jwtTokenRepository.GenerarTokensAccesso(usuario);

    }

    public async Task<ResponseJwtDto> RefreshAccessTokenAsync(RefreshTokenDto refreshToken)
    {
        string emailUsuario = _jwtTokenRepository.ValidarRefreshToken(refreshToken.RefreshToken);
        UsuarioDto usuario = await _usuarioRepository.ObtenerUsuarioPorEmailAsync(emailUsuario); 
           

        return _jwtTokenRepository.GenerarTokensAccesso(usuario,false);
    }

    public async Task<string> GenerarEnlaceRestablecimientoAsync(string email)
    {

        string token = await _usuarioRepository.GenerarTokenRestablecimientoContrasena(email);
        string encodeToken = HttpUtility.UrlEncode(token);
        string encodedEmail = HttpUtility.UrlEncode(email);
        string url = $"https://localhost:5001/api/auth/restablecer-contrasena?token={encodeToken}&email={encodedEmail}";

        await _notificacionRepository.EnviarNotificacionEmailAsync(new ParamatrosNotificacionDto
        {
            Destinatario = email,
            Asunto = "Restablecimiento de contraseña",
            Mensaje = $"<p>Para restablecer tu contraseña, haz clic en el siguiente enlace:</p><a href='{url}'>Restablecer contraseña</a>"
        });

        return url;

    }

    public async Task RestablecerContrasenaAsync(RestablecerContrasenaDto restablecerContrasena)
    {
        restablecerContrasena.Token = HttpUtility.UrlDecode(restablecerContrasena.Token);
        await _usuarioRepository.RestablecerContrasena(restablecerContrasena);
    }
}
