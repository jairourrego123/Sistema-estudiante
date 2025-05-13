
namespace Domain.Ports.IMateriaRepository;

public interface IMateriaQueryRepository
{
    Task<Materia?> ObtenerPorIdAsync(Guid id);
    Task<List<Materia>> ObtenerTodosAsync();
    Task<List<Materia>> ObtenerPorIdsAsync(IEnumerable<Guid> ids);
}