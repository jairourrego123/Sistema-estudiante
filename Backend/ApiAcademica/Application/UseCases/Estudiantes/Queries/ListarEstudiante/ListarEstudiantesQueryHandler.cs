using MediatR;
using Domain.Entities;
using Domain.Ports.IEstudianteRepository;

namespace Application.UseCases.Estudiantes.Queries.ListarEstudiante;

public class ListarEstudiantesQueryHandler
    : IRequestHandler<ListarEstudiantesQuery, List<Estudiante>>
{
    private readonly IEstudianteQueryRepository _estudianteQueryRepository;

    public ListarEstudiantesQueryHandler(IEstudianteQueryRepository estudianteQueryRepository)
        => _estudianteQueryRepository = estudianteQueryRepository;

    public async Task<List<Estudiante>> Handle(
        ListarEstudiantesQuery request,
        CancellationToken cancellationToken)
    {
        return await _estudianteQueryRepository.ObtenerTodosAsync();
    }
}
