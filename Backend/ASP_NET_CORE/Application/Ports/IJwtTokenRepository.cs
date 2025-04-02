using Application.Dtos.AuthDtos;
using Application.UseCases.Auth;
using System.Security.Claims;

namespace Application.Ports;
public interface IJwtTokenRepository
{
    ResponseJwtDto GenerarTokensAccesso(UsuarioDto usuario,bool incluirRefresh = true);
    string? ValidarRefreshToken(string refreshToken);
    string? GenerarTokenRestablecimientoContrasena(string email);
    string? ValidarTokenRestablecimientoContrasena(string token);
}
