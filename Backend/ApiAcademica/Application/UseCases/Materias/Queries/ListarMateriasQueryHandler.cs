

using Application.Dtos;
using Domain.Ports.IMateriaRepository;
using MediatR;

namespace Application.UseCases.Materias.Queries;

public class ListarMateriasQueryHandler
        : IRequestHandler<ListarMateriasQuery, List<MateriaConProfesorDto>>
{
    private readonly IMateriaQueryRepository _queryRepo;

    public ListarMateriasQueryHandler(IMateriaQueryRepository queryRepo)
    {
        _queryRepo = queryRepo;
    }

    public async Task<List<MateriaConProfesorDto>> Handle(ListarMateriasQuery request,CancellationToken cancellationToken)
    {

        List<Materia> materias = await _queryRepo.ObtenerTodosAsync();
        return MapMateriasConProfesor(materias);
    }

    private static List<MateriaConProfesorDto> MapMateriasConProfesor(List<Materia> materias)
    {
        return materias.Select(m => new MateriaConProfesorDto(m.Id, m.Nombre, m.Creditos, m.ProfesorId, m.Profesor.Nombre))
           .ToList();
    }
}

