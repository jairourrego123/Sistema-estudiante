using Application.Dtos.Auth;

namespace Application.Ports.Repositorys;
public interface IJwtTokenRepository
{
    ResponseJwtDto GenerarTokensAccesso(UsuarioDto usuario,bool incluirRefresh = true);
    string ValidarRefreshToken(string refreshToken);
 

}
