namespace Application.Dtos.Auth;

public class UsuarioDto: BaseUsername
{
    public Guid Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;

}
