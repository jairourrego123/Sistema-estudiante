namespace Application.Dtos.AuthDtos;

public class UsuarioDto
{
    public Guid Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public required string Email { get; set; }

}
