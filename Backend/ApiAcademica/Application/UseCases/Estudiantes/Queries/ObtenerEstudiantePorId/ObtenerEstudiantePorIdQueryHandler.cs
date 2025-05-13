using Domain.Entities;
using Domain.Ports.IEstudianteRepository;
using MediatR;


namespace Application.UseCases.Estudiantes.Queries.ObtenerEstudiantePorId;

public class ObtenerEstudiantePorIdQueryHandler
    : IRequestHandler<ObtenerEstudiantePorIdQuery, Estudiante?>
{
    private readonly IEstudianteQueryRepository _estudianteQueryRepository;

    public ObtenerEstudiantePorIdQueryHandler(IEstudianteQueryRepository estudianteQueryRepository)
        => _estudianteQueryRepository = estudianteQueryRepository;


    public async Task<Estudiante?> Handle(
        ObtenerEstudiantePorIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _estudianteQueryRepository.ObtenerPorIdAsync(request.Id);
    }
}
