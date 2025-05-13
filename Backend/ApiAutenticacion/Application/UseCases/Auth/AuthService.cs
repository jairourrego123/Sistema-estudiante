using Application.Dtos.Auth;
using Application.Dtos.Notificacion;
using Application.Ports.Repositorys;
using Application.Ports.Services;
using System.Text;
using System.Web;
using Microsoft.AspNetCore.WebUtilities;
using Shared.Const.EmailConst;
using System.Web.Helpers;
using System;
using Newtonsoft.Json.Linq;

namespace Application.UseCases.Auth;

public class AuthService : IAuthRepository
{
    private readonly IJwtTokenRepository _jwtTokenRepository;
    private readonly IUsuarioRepository _usuarioRepository; 
    private readonly INotificacionService _notificacionRepository;
    private readonly IConfiguracionAppService _configuracionAppService;
    public AuthService(
       IJwtTokenRepository jwtTokenRepository,
       IUsuarioRepository usuarioRepository,
       INotificacionService notificacionRepository,
       IConfiguracionAppService configuracionAppService )
    {
        _jwtTokenRepository = jwtTokenRepository;
        _usuarioRepository = usuarioRepository;
        _notificacionRepository = notificacionRepository;
        _configuracionAppService = configuracionAppService;

    }

    public async Task RegistrarUsuarioAsync(RegistroDto registroDto)
    {
        await _usuarioRepository.CrearUsuarioAsync(registroDto);
        await NotificarRegistroUsuario(registroDto);
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

    public async Task GenerarEnlaceRestablecimientoAsync(string email)
    {
        string token = await _usuarioRepository.GenerarTokenRestablecimientoContrasena(email);
        string url = ConstruirUrlRestablecimiento(email, token);
        await NotificarRestablecimientoContrasena(email, url);
    }


    public async Task RestablecerContrasenaAsync(RestablecerContrasenaDto restablecerContrasena)
    {

        restablecerContrasena.Token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(restablecerContrasena.Token));
        await _usuarioRepository.RestablecerContrasena(restablecerContrasena);
    }


    private async Task NotificarRegistroUsuario(RegistroDto registroDto)
    {
        string url = _configuracionAppService.ObtenerUrlFrontend(); 
        await _notificacionRepository.EnviarNotificacionEmailAsync(new ParamatrosNotificacionDto
        {
            Destinatario = registroDto.Email,
            Asunto = EmailSubjects.Bienvenida,
            Plantilla = EmailTemplates.Bienvenida,
            Parametros = new Dictionary<string, string>
        {
            { "nombre", $"{registroDto.Nombre} {registroDto.Apellido}" },
            { "url", url }  
        }
        });
    }


    private async Task NotificarRestablecimientoContrasena(string email, string url)
    {
        await _notificacionRepository.EnviarNotificacionEmailAsync(new ParamatrosNotificacionDto
        {
            Destinatario = email,
            Asunto = EmailSubjects.RestablecerContrasena,
            Plantilla = EmailTemplates.RestablecerContrasena,
            Parametros = new Dictionary<string, string>
        {
            { "url", url }
        }
        });
    }

    private string ConstruirUrlRestablecimiento(string email, string token)
    {
        string baseUrlFrontend = _configuracionAppService.ObtenerUrlFrontend();
        string encodedEmail = HttpUtility.UrlEncode(email);
        string encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

        return $"{baseUrlFrontend}/restablecer-contrasena?token={encodedToken}&email={encodedEmail}";
    }
}
