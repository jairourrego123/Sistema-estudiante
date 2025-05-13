using Domain.Exceptions;

using Shared.Const;

namespace Domain.Entities;
public class Estudiante : DomainBase
{
    public string UserId { get; private set; } = null!;
    public string Nombre { get; private set; } = null!;
    public string Apellido { get; private set; } = null!;
    public List<Inscripcion> Inscripciones { get; private set; } = new();
    public Estudiante()
    {
        
    }
    public Estudiante(string userId, string nombre, string apellido)
    {
        UserId = userId;
        CambiarNombre(nombre);
        CambiarApellido(apellido);
    }

    public void CambiarNombre(string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ValorVacioException("Nombre");

        Nombre = nombre.Trim();
    }

    public void CambiarApellido(string apellido)
    {
        if (string.IsNullOrWhiteSpace(apellido))
            throw new ValorVacioException("Apellido");

        Apellido = apellido.Trim();
    }

    public void InscribirAMateria(Materia materia)
    {
        if (Inscripciones.Count >= Const.LimiteMateriasXEstudiante)
            throw new LimiteMateriasInscritasException(Const.LimiteMateriasXEstudiante);

        if (Inscripciones.Any(i => i.Materia.ProfesorId == materia.ProfesorId))
            throw new ProfesorExistenteException();

        Inscripciones.Add(new Inscripcion
        {
            MateriaId = materia.Id,
            Materia = materia,
            FechaGrabacion = DateTime.UtcNow,
        });
    }
}
