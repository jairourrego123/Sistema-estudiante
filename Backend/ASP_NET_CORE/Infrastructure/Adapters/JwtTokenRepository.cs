using Application.Dtos.Auth;
using Application.Exceptions;
using Application.Ports.Repositorys;
using Infrastructure.Adapters.GenericRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shared.Resources;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Adapters;

[Repository]
public class JwtTokenRepository : IJwtTokenRepository
{
    private readonly IConfiguration _configuration;

    public JwtTokenRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public ResponseJwtDto GenerarTokensAccesso(UsuarioDto usuario,bool incluirRefreshToken = true)
    {

        string expirationMinutes = _configuration["Jwt:ExpirationMinutes"]!;
        string expirationDaysRefresh = _configuration["Jwt:ExpirationDaysRefresh"]!;
        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]!));
        SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

        string accessToken = GenerarAccessToken(usuario, expirationMinutes, credentials, tokenHandler);

        string? refreshToken = incluirRefreshToken ? GenerarRefreshToken(usuario, expirationDaysRefresh, credentials, tokenHandler) : null;

        return new ResponseJwtDto(accessToken, refreshToken);

    }


    private string GenerarAccessToken(UsuarioDto usuario, string ExpirationMinutes, SigningCredentials credentials, JwtSecurityTokenHandler tokenHandler)
    {
        List<Claim> accessClaims = [
        new(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
        new(ClaimTypes.Email, usuario.Email),
        new("nombre", usuario.Nombre),
        new("apellido", usuario.Apellido)
    ];
        SecurityTokenDescriptor? accessTokenDescriptor = new()
        {
            Subject = new ClaimsIdentity(accessClaims),
            Expires = DateTime.UtcNow.AddMinutes(int.Parse(ExpirationMinutes)),
            SigningCredentials = credentials,
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"]
        };

        return tokenHandler.WriteToken(tokenHandler.CreateToken(accessTokenDescriptor));

    }
    private string GenerarRefreshToken(UsuarioDto usuario, string ExpirationDaysRefresh, SigningCredentials credentials, JwtSecurityTokenHandler tokenHandler)
    {
        List<Claim> refreshClaims = [
        new(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
        new(ClaimTypes.Email, usuario.Email.ToString()),
        new("type", "refresh")
        ];

        SecurityTokenDescriptor refreshTokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(refreshClaims),
            Expires = DateTime.UtcNow.AddDays(int.Parse(ExpirationDaysRefresh)),
            SigningCredentials = credentials,
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"]
        };

        return tokenHandler.WriteToken(tokenHandler.CreateToken(refreshTokenDescriptor));

    }

    public string ValidarRefreshToken(string refreshToken)
    {
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        byte[] key = Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]!);

        try
        {
            var validacionParametros =  new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };

            ClaimsPrincipal claimsToken = tokenHandler.ValidateToken(refreshToken, validacionParametros, out _);

            return claimsToken == null
                ? throw new BusinessException(Messages.UsuarioNoEncontrado)
                : claimsToken.FindFirstValue(ClaimTypes.Email);
        }
        catch
        {
            throw new UnauthorizedException(Messages.SesionCaducada);

        }
    }

}
