using Domain.Entities;

namespace Domain.Ports.IProfesorRepository;

public interface IProfesorRepository
{
    Task CrearAsync(Profesor profesor);
    Task ActualizarAsync(Profesor profesor);
    Task EliminarAsync(Guid id);
    Task GuardarCambiosAsync();

}
