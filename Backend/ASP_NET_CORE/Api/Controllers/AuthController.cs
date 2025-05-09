using Application.Dtos.Auth;
using Application.Ports.Repositorys;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authService;

        public AuthController(IAuthRepository authService)
        {
            _authService = authService;
        }

        /// <summary>Registra un nuevo usuario.</summary>
        [HttpPost("registrar-usuario")]
        public async Task<IActionResult> RegistrarUsuario([FromBody] RegistroDto dto)
        {
            await _authService.RegistrarUsuarioAsync(dto);
            return Created();
        }

        /// <summary>Inicia sesión y retorna token JWT.</summary>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var response = await _authService.LoginAsync(dto);
            return Ok(response);
        }

        /// <summary>Refresca el token de acceso.</summary>
        [HttpPost("token/refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDto dto)
        {
            var token = await _authService.RefreshAccessTokenAsync(dto);
            return Ok(token);
        }

        /// <summary>Genera un enlace de restablecimiento de contraseña y lo envía al correo.</summary>
        [HttpPost("password/enlace-restablecimiento")]
        public async Task<IActionResult> GenerarEnlaceRestablecimiento( [FromBody] BaseUsernameDto username)
        {
            await _authService.GenerarEnlaceRestablecimientoAsync(username.Email);
            return Ok();
        }


        [HttpPost("password/restablecer")]
        public async Task<IActionResult> RestaurarContraseña(RestablecerContrasenaDto restablecerContrasenaDto)
        {
            await _authService.RestablecerContrasenaAsync(restablecerContrasenaDto);
            return Ok();
        }



        /// <summary>Información del usuario autenticado.</summary>
        [Authorize]
        [HttpGet("debug-token")]
        public IActionResult DebugToken()
        {
            return Ok(new
            {
                Autenticado = User.Identity?.IsAuthenticated,
                Usuario = User.Identity?.Name,
                Claims = User.Claims.Select(c => new { c.Type, c.Value })
            });
        }

    }
}
