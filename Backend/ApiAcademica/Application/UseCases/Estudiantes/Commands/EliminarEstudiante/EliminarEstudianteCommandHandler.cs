using Domain.Exceptions;
using MediatR;
using Domain.Entities;
using Domain.Ports.IEstudianteRepository;

namespace Application.UseCases.Estudiantes.Commands.EliminarEstudiante;

public class EliminarEstudianteCommandHandler
    : IRequestHandler<EliminarEstudianteCommand,Unit>
{
    private readonly IEstudianteQueryRepository _estudianteQueryRepository;
    private readonly IEstudianteRepository _estudianteRepository;

    public EliminarEstudianteCommandHandler(IEstudianteQueryRepository estudianteQueryRepository, IEstudianteRepository estudianteRepository)
    {
        _estudianteQueryRepository = estudianteQueryRepository;
        _estudianteRepository = estudianteRepository;
    }

    public async Task<Unit> Handle(
        EliminarEstudianteCommand request,
        CancellationToken cancellationToken)
    {
        Estudiante estudiante = await _estudianteQueryRepository.ObtenerPorIdAsync(request.Id)
            ?? throw new NoEncontradoException("Estudiante");

        await _estudianteRepository.EliminarAsync(estudiante.Id);
        return Unit.Value;
    }
}
