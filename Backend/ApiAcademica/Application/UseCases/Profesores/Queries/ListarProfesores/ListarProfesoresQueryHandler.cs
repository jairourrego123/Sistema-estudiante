using Domain.Ports.IProfesorRepository;
using MediatR;

namespace Application.UseCases.Profesores.Queries.ListarProfesores;

public class ListarProfesoresQueryHandler : IRequestHandler<ListarProfesoresQuery, List<Profesor>>
{
    private readonly IProfesorQueryRepository _profesorQueryRepository;
    public ListarProfesoresQueryHandler(IProfesorQueryRepository profesorQueryRepository)
    {
        _profesorQueryRepository = profesorQueryRepository;
    }

    public Task<List<Profesor>> Handle( ListarProfesoresQuery request,CancellationToken ct)
    {
        return _profesorQueryRepository.ObtenerTodosAsync();
    }
}