using Domain.Ports.IProfesorRepository;
using MediatR;


namespace Application.UseCases.Profesores.Queries.ObtenerProfesorPorIdQuery;

public class ObtenerProfesorPorIdQueryHandler :
       IRequestHandler<ObtenerProfesorPorIdQuery, Profesor?>
{
    private readonly IProfesorQueryRepository _profesorQueryRepository;
    public ObtenerProfesorPorIdQueryHandler(IProfesorQueryRepository profesorQueryRepository)
        => _profesorQueryRepository = profesorQueryRepository;

    public Task<Profesor?> Handle(
        ObtenerProfesorPorIdQuery request,
        CancellationToken ct)
        => _profesorQueryRepository.ObtenerPorIdAsync(request.Id);
}