using Application.Dtos;
using Domain.Entities;
using Domain.Ports.IEstudianteRepository;
using Domain.Ports.IInscripcionRepository;
using MediatR;

namespace Application.UseCases.Estudiantes.Queries.ObtenerCompañeros;

public class ListarCompanerosQueryHandler
    : IRequestHandler<ListarCompanerosQuery, List<NombreDto>>
{
    private readonly IEstudianteQueryRepository _estudianteQueryRepository;
    private readonly IInscripcionQueryRepository _inscripcionQueryRepository;

    public ListarCompanerosQueryHandler(IEstudianteQueryRepository estudianteQueryRepository, IInscripcionQueryRepository inscripcionQueryRepository)
    {
        _estudianteQueryRepository = estudianteQueryRepository;
        _inscripcionQueryRepository = inscripcionQueryRepository;
    }

    public async Task<List<NombreDto>> Handle(
        ListarCompanerosQuery request,
        CancellationToken cancellationToken)
    {
        Estudiante estudiante = await _estudianteQueryRepository.ObtenerPorUserIdAsync(request.UserId)
         ?? throw new KeyNotFoundException("Estudiante no encontrado.");

        List<Inscripcion> inscripciones = await _inscripcionQueryRepository.ObtenerPorEstudianteAsync(estudiante.Id);

        List<Guid> materiasIds = inscripciones
               .Select(i => i.MateriaId)
               .Distinct()
               .ToList();

        HashSet<string> compañeros = await ObtenerCompañeros(request, materiasIds);
        return compañeros.Select(n => new NombreDto(n)).ToList();

    }

    private async Task<HashSet<string>> ObtenerCompañeros(ListarCompanerosQuery request, List<Guid> materiasIds)
    {
        HashSet<string> compañeros = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (var matId in materiasIds)
        {
            List<string> lista = await _estudianteQueryRepository.ListarCompañerosAsync(matId, request.UserId);
            foreach (var nombre in lista)
                compañeros.Add(nombre);
        }

        return compañeros;
    }
}
