

using Application.Dtos.Auth;
using Application.Exceptions;
using Application.Ports.Repositorys;
using Infrastructure.Adapters.GenericRepository;
using Infrastructure.DataSource.Configuracion.Identity;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Shared.Resources;
using System.Text;
using System.Web;

namespace Infrastructure.Adapters;

[Repository]
public class UsuarioRepository : IUsuarioRepository
{
    private readonly UserManager<UsuarioIdentity> _userManager;

    public UsuarioRepository(UserManager<UsuarioIdentity> userManager)
    {
        _userManager = userManager;
    }
    /// <summary>
    /// Meotodo para rear Un usuario en el sistema
    /// </summary>
    /// <param name="registroDto">Objeto del tipo RegistroDto con catos basicos de registro</param>
    /// <exception cref="BusinessException">Excepcion en caso de que el usuario exista </exception>
    public async Task<string> CrearUsuarioAsync(RegistroDto registroDto)
    {
        UsuarioIdentity? usuario = await _userManager.FindByEmailAsync(registroDto.Email);
        if (usuario != null) throw new BusinessException(Messages.UsuarioExistente);

        usuario = MapService.MapRegistroUsuarioDtoToUsuarioIdentity(registroDto);
        IdentityResult resultado = await _userManager.CreateAsync(usuario, registroDto.Password);
        if (!resultado.Succeeded)
        {
            string errores = string.Join(", ", resultado.Errors.Select(e => e.Description));
            throw new BusinessException(errores);
            
        }
        return usuario.Id.ToString();

    }

    /// <summary>
    /// Metodo para obtener un usuario segun el email
    /// </summary>
    /// <param name="email">Email del usuario a buscar</param>
    /// <returns>Informacion del usuario </returns>
    /// <exception cref="BusinessException">Excepcion en caso de que el usuario no exista</exception>
    public async Task<UsuarioDto> ObtenerUsuarioPorEmailAsync(string email)
    {
        UsuarioIdentity usuario = await _userManager.FindByEmailAsync(email) 
            ?? throw new BusinessException(Messages.UsuarioNoEncontrado);

        return MapService.MapUsuarioIdentityToUsuarioDto(usuario);
    }

    /// <summary>
    /// Metodo para validar las credenciales de un usuario 
    /// </summary>
    /// <param name="loginDto">Objeto del tipo LoginDto con informacion del login </param>
    /// <returns>Informacion del usuario</returns>
    /// <exception cref="UnauthorizedAccessException"></exception>
    public async Task<UsuarioDto> ValidarCredenciales(LoginDto loginDto)
    {
        UsuarioIdentity? usuario = await _userManager.FindByEmailAsync(loginDto.Email);

        if (usuario == null || !await _userManager.CheckPasswordAsync(usuario, loginDto.Password))
        {
            throw new UnauthorizedException(Messages.CredencialesInvalidas);
        }
        return MapService.MapUsuarioIdentityToUsuarioDto(usuario); ;

    }

    /// <summary>
    /// Metodo para generar token de restablecimiento de contraseña 
    /// </summary>
    /// <param name="email">Email del usuario que busca restablecer su contraseña</param>
    /// <returns>String del token </returns>
    /// <exception cref="BusinessException">Excepcion en caso de que el usuario no exista</exception>
    public async Task<string> GenerarTokenRestablecimientoContrasena(string email)
    {
        UsuarioIdentity usuario = await _userManager.FindByEmailAsync(email) 
            ?? throw new BusinessException(Messages.UsuarioNoEncontrado);
        string token = await _userManager.GeneratePasswordResetTokenAsync(usuario);
        string tokenCodificado = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

        return tokenCodificado;
    }

    /// <summary>
    /// Metodo para restablecer la contraseña de un usuario
    /// </summary>
    /// <param name="restablecerContrasena">Objeto del tipo RestablecerContrasenaDto</param>
    /// <exception cref="BusinessException">Excepcion en caso de que el usuario no exista o presente o la informacion suministrada en el objeto sea incorrecta</exception>
    public async Task  RestablecerContrasena(RestablecerContrasenaDto restablecerContrasena)
    {
        try
        {
            UsuarioIdentity usuario = await _userManager.FindByEmailAsync(restablecerContrasena.Email)
            ?? throw new BusinessException(Messages.UsuarioNoEncontrado);

            string tokenDecodificado = Encoding.UTF8.GetString(
                WebEncoders.Base64UrlDecode(restablecerContrasena.Token)
            );
            IdentityResult resultado = await _userManager.ResetPasswordAsync(usuario, tokenDecodificado, restablecerContrasena.NuevaContrasena);
            if (!resultado.Succeeded)
            {
                string errores = string.Join(", ", resultado.Errors.Select(e => e.Description));
                throw new BusinessException(errores);
            }
        }
        catch (FormatException ex)
        {
            throw new BusinessException($"Token inválido: {ex.Message}");
        }
        catch (Exception ex)
        {
            throw new BusinessException($"Error al restablecer contraseña: {ex.Message}");
        }
    } 



}
