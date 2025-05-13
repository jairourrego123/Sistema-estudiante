
namespace Domain.Ports.IProfesorRepository;

public interface IProfesorQueryRepository
{
    Task<Profesor?> ObtenerPorIdAsync(Guid id);
    Task<List<Profesor>> ObtenerTodosAsync();
}