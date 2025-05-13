using Domain.Entities;

namespace Domain.Ports.IEstudianteRepository;

public interface IEstudianteQueryRepository
{
    Task<Estudiante> ObtenerPorUserIdAsync(string userId);
    Task<Estudiante?> ObtenerPorIdAsync(Guid id);
    Task<List<Estudiante>> ObtenerTodosAsync();
    Task<List<string>> ListarCompañerosAsync(Guid materiaId, string userId);
}
