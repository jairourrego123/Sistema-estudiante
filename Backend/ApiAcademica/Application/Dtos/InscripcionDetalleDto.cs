using Domain.Entities;

namespace Application.Dtos;

public class InscripcionDetalleDto
{
    public Guid InscripcionId { get; set; }
    public Guid EstudianteId { get; set; }
    public Guid MateriaId { get; set; }
    public string MateriaNombre { get; set; } = null!;
    public int Creditos { get; set; }
    public Guid ProfesorId { get; set; }
    public string ProfesorNombre { get; set; } = null!;

    public InscripcionDetalleDto()
    {
        
    }
    public InscripcionDetalleDto(Inscripcion i)
    {
        InscripcionId = i.Id;
        EstudianteId = i.EstudianteId;
        MateriaId = i.MateriaId;
        MateriaNombre = i.Materia.Nombre;
        Creditos = i.Materia.Creditos;
        ProfesorId = i.Materia.Profesor.Id;
        ProfesorNombre = i.Materia.Profesor.Nombre;
    }
}
