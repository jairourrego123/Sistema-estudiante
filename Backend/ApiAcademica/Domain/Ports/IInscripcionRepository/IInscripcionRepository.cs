using Domain.Entities;


namespace Domain.Ports.IInscripcionRepository;

public interface IInscripcionRepository
{
    Task CrearAsync(Inscripcion inscripcion);
    Task EliminarAsync(Guid inscripcionId);
    Task GuardarCambiosAsync();
}