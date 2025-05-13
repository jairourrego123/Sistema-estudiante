using Domain.Entities;

namespace Domain.Ports.IEstudianteRepository;

public interface IEstudianteRepository
{
    Task CrearAsync(Estudiante e);
    Task ActualizarAsync(Estudiante e);
    Task EliminarAsync(Guid id);
    Task GuardarCambiosAsync();
}