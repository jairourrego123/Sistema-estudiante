using Application.Dtos.AuthDtos;
using Application.Ports;
using Application.UseCases.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authService;

        public AuthController(IAuthRepository authService) 
        {
            _authService = authService;
        }

        /// <summary>
        /// Registra un nuevo usuario.
        /// </summary>
        [HttpPost("register")]
        public async Task<IActionResult> RegistrarUsuario([FromBody] RegistroDto dto)
        {
            try
            {
                await _authService.RegistrarUsuarioAsync(dto);
                return Ok();
            }
            catch (Exception ex) {
                return BadRequest(new { message = ex.Message });

            }
            
        }

        /// <summary>
        /// Inicia sesión y genera un token JWT.
        /// </summary>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                ResponseJwtDto response = await _authService.LoginAsync(loginDto);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });

            }
          
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshAccessToken([FromBody] RefreshTokenDto refreshTokenDto)
        {
            try
            {
                ResponseJwtDto responseJwt = await _authService.RefreshAccessTokenAsync(refreshTokenDto);
                return Ok(responseJwt);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }


        //[HttpPost("refresh-token")]
        //public IActionResult RefreshToken([FromBody] string refreshToken)
        //{
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var validationParameters = JwtConfigHelper.GetTokenValidationParameters(_configuration, isRefreshToken: true);

        //    try
        //    {
        //        var principal = tokenHandler.ValidateToken(refreshToken, validationParameters, out SecurityToken validatedToken);
        //        return principal;
        //    }
        //    catch
        //    {
        //        return null; 
        //    }

        //}

        /// <summary>
        /// Prueba de ruta protegida
        /// </summary>
        [Authorize]
        [HttpGet("protected")]
        public IActionResult ProtectedRoute()
        {
            return Ok(new { message = "Acceso autorizado" });
        }

        [HttpGet("test")]
        public IActionResult TestAuth()
        {
            return Ok(new { User = User.Identity?.Name, IsAuthenticated = User.Identity?.IsAuthenticated });
        }

        [Authorize]
        [HttpGet("debug-token")]
        public IActionResult DebugToken()
        {
            return Ok(new
            {
                IsAuthenticated = User.Identity?.IsAuthenticated,
                UserName = User.Identity?.Name,
                Claims = User.Claims.Select(c => new { c.Type, c.Value })
            });
        }


    }
}
