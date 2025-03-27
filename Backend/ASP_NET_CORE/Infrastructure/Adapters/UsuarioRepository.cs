

using Application.Dtos.AuthDtos;
using Application.Ports;
using Infrastructure.Adapters.GenericRepository;
using Infrastructure.DataSource.Configuracion.Identity;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Adapters;

[Repository]
public class UsuarioRepository : IUsuarioRepository
{
    private readonly UserManager<UsuarioIdentity> _userManager;
    private readonly IJwtTokenRepository _jwtTokenService;

    public UsuarioRepository(UserManager<UsuarioIdentity> userManager, IJwtTokenRepository jwtTokenService)
    {
        _userManager = userManager;
        _jwtTokenService = jwtTokenService;
    }

    public async Task CrearUsuarioAsync(RegistroDto registroDto)
    {
        UsuarioIdentity usuario = MapService.MapRegistroUsuarioDtoToUsuarioIdentity(registroDto);
        IdentityResult resultado = await _userManager.CreateAsync(usuario, registroDto.Password);

        if (!resultado.Succeeded)
        {
            string errores = string.Join(", ", resultado.Errors.Select(e => e.Description));
            throw new ($"Error en el registro: {errores}");
        }
    }

    public async Task<UsuarioDto?> ObtenerUsuarioPorEmailAsync(string email)
    {
        UsuarioIdentity? usuario = await _userManager.FindByEmailAsync(email);
        if (usuario == null) return null;

        return MapService.MapUsuarioIdentityToUsuarioDto(usuario);
    }


    public async Task<UsuarioDto> ValidarCredenciales(LoginDto loginDto)
    {
        UsuarioIdentity? usuario = await _userManager.FindByEmailAsync(loginDto.Email);
        
        if (usuario == null || !await _userManager.CheckPasswordAsync(usuario, loginDto.Password))
        {
            throw new Exception("El usuario o la contraseña son incorrectos");

        }
        return MapService.MapUsuarioIdentityToUsuarioDto(usuario); ;

    }


}
