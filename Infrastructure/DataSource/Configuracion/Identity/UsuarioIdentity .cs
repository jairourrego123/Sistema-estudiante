using Microsoft.AspNetCore.Identity;

namespace Infrastructure.DataSource.Configuracion.Identity;

public class UsuarioIdentity : IdentityUser<Guid>
{
    public required string Nombre { get; set; }
    public required string Apellido { get; set; }
    public DateTime FechaRegistro { get; set; }
}