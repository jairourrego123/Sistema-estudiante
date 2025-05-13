
using Domain.Entities;
using Domain.Exceptions;
using Domain.Ports.IEstudianteRepository;
using Domain.Ports.IInscripcionRepository;
using MediatR;

namespace Application.UseCases.Inscripciones.Command.DesinscribirMaterias;

public class DesinscribirMateriaCommandHandler : IRequestHandler<DesinscribirMateriaCommand,Unit>
{
    private readonly IEstudianteQueryRepository _estudianteQueryRepository;
    private readonly IInscripcionQueryRepository _inscripcionQueryRepository;
    private readonly IInscripcionRepository _inscripcionRepository;

    public DesinscribirMateriaCommandHandler(
        IEstudianteQueryRepository estudianteQueryRepository,
        IInscripcionQueryRepository inscripcionQueryRepository,
        IInscripcionRepository inscripcionRepository)
    {
        _estudianteQueryRepository = estudianteQueryRepository;
        _inscripcionQueryRepository = inscripcionQueryRepository;
        _inscripcionRepository = inscripcionRepository;
    }

    public async Task<Unit> Handle(DesinscribirMateriaCommand request, CancellationToken ct)
    {
        Estudiante estudiante = await ValidarEstudiante(request);
        Inscripcion ins = await ValidarInscripcion(request, estudiante);
        await _inscripcionRepository.EliminarAsync(ins.Id);
        return Unit.Value;
    }

    private async Task<Estudiante> ValidarEstudiante(DesinscribirMateriaCommand request)
    {
        return await _estudianteQueryRepository.ObtenerPorUserIdAsync(request.UserId)
            ?? throw new NoEncontradoException("Estudiante");
    }

    private async Task<Inscripcion> ValidarInscripcion(DesinscribirMateriaCommand request, Estudiante estudiante)
    {
        List<Inscripcion> inscripciones = await _inscripcionQueryRepository.ObtenerPorEstudianteAsync(estudiante.Id);
        Inscripcion inscripcion = inscripciones.FirstOrDefault(i => i.MateriaId == request.MateriaId)
            ?? throw new MateriaNoInscriptaException();
        return inscripcion;
    }
}
