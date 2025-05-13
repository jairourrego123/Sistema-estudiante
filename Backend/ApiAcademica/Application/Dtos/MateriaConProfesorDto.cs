namespace Application.Dtos;

public class MateriaConProfesorDto
{
    public Guid Id { get; init; }
    public string Nombre { get; init; } = null!;
    public int Creditos { get; init; }
    public Guid ProfesorId { get; init; }
    public string ProfesorNombre { get; init; } = null!;

    public MateriaConProfesorDto(Guid id, string nombre, int creditos,Guid profesorId, string profesorNombre)
    {
        Id = id;
        Nombre = nombre;
        Creditos = creditos;
        ProfesorId = profesorId;
        ProfesorNombre = profesorNombre;
    }
}
