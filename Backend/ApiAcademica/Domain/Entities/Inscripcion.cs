namespace Domain.Entities;

public class Inscripcion : DomainBase
{
    public Guid EstudianteId { get; set; }
    public Estudiante Estudiante { get; set; } = null!;
    public Guid MateriaId { get; set; }
    public Materia Materia { get; set; } = null!;
}
