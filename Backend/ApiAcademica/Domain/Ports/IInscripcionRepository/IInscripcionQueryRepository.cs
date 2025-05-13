
using Domain.Entities;

namespace Domain.Ports.IInscripcionRepository;

public interface IInscripcionQueryRepository
{
    /// <summary>
    /// Obtener todas las inscripciones de un estudiante.
    /// </summary>
    Task<List<Inscripcion>> ObtenerPorEstudianteAsync(Guid estudianteId);

}
