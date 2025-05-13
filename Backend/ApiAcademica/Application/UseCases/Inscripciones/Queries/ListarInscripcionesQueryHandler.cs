using Application.Dtos;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Ports.IEstudianteRepository;
using Domain.Ports.IInscripcionRepository;
using MediatR;


namespace Application.UseCases.Inscripciones.Queries;

public class ListarInscripcionesQueryHandler
    : IRequestHandler<ListarInscripcionesQuery, List<InscripcionDetalleDto>>
{
    private readonly IEstudianteQueryRepository _estudianteQueryRepository;
    private readonly IInscripcionQueryRepository _inscripcionQueryRepository;

    public ListarInscripcionesQueryHandler(
        IEstudianteQueryRepository estudianteQueryRepository,
        IInscripcionQueryRepository inscripcionQueryRepository)
    {
        _estudianteQueryRepository = estudianteQueryRepository;
        _inscripcionQueryRepository = inscripcionQueryRepository;
    }

    public async Task<List<InscripcionDetalleDto>> Handle(
        ListarInscripcionesQuery request,
        CancellationToken cancellationToken)
    {
        Estudiante estudiante = await _estudianteQueryRepository.ObtenerPorUserIdAsync(request.UserId)
            ?? throw new NoEncontradoException("Estudiante");

        List<Inscripcion> inscripciones = await _inscripcionQueryRepository.ObtenerPorEstudianteAsync(estudiante.Id);

        List<InscripcionDetalleDto> dtoList = inscripciones.Select(i => new InscripcionDetalleDto(i)).ToList();

        return dtoList;
    }
}
