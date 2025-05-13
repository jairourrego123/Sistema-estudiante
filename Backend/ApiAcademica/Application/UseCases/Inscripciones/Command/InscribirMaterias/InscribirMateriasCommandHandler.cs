

using Domain.Exceptions;
using Domain.Entities;
using Domain.Ports.IEstudianteRepository;
using Domain.Ports.IInscripcionRepository;
using Domain.Ports.IMateriaRepository;
using MediatR;

namespace Application.UseCases.Inscripciones.Command.InscribirMaterias;

public class InscribirMateriasCommandHandler : IRequestHandler<InscribirMateriasCommand,Unit>
{
    private readonly IEstudianteQueryRepository _estudianteQueryRepository;
    private readonly IMateriaQueryRepository _materiaQueryRepository;
    private readonly IInscripcionRepository _inscripcionRepository;

    public InscribirMateriasCommandHandler(
        IEstudianteQueryRepository estudianteQueryRepository,
        IMateriaQueryRepository materiaQueryRepository,
        IInscripcionRepository inscripcionRepository)
    {
        _estudianteQueryRepository = estudianteQueryRepository;
        _materiaQueryRepository = materiaQueryRepository;
        _inscripcionRepository = inscripcionRepository;
    }

    public async Task<Unit> Handle(InscribirMateriasCommand request, CancellationToken ct)
    {
        Estudiante estudiante = await ValidarEstudiante(request);
        List<Materia> materias = await ValidarMaterias(request);
        await InscribirMateria(estudiante, materias);
        return Unit.Value;
    }

    private async Task<List<Materia>> ValidarMaterias(InscribirMateriasCommand request)
    {
        List<Materia> materias = await _materiaQueryRepository.ObtenerPorIdsAsync(request.MateriaIds);
        if (materias.Count != request.MateriaIds.Count)
            throw new NoEncontradoException("Materia");
        return materias;
    }

    private async Task<Estudiante> ValidarEstudiante(InscribirMateriasCommand request)
    {
        return await _estudianteQueryRepository.ObtenerPorUserIdAsync(request.UserId)
            ?? throw new NoEncontradoException("Estudiante");
    }

    private async Task InscribirMateria(Estudiante estudiante, List<Materia> materias)
    {
        foreach (Materia materia in materias)
        {
            estudiante.InscribirAMateria(materia);
            Inscripcion inscripcion = new Inscripcion
            {
                EstudianteId = estudiante.Id,
                MateriaId = materia.Id,
                FechaGrabacion = DateTime.UtcNow
            };
            await _inscripcionRepository.CrearAsync(inscripcion);
        }
    }
}
