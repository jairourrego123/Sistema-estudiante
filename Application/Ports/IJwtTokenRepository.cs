using Application.Dtos.AuthDtos;
using Application.UseCases.Auth;
using System.Security.Claims;

namespace Application.Ports;
public interface IJwtTokenRepository
{
    ResponseJwtDto GenerarTokens(UsuarioDto usuario,bool incluirRefresh = true);
    string?ValidarRefreshToken(string refreshToken);

}
