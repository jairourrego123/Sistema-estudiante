using Domain.Exceptions;
using Domain.Entities;
using MediatR;
using Domain.Ports.IEstudianteRepository;

namespace Application.UseCases.Estudiantes.Commands.CrearEstudiante;

public class EditarEstudianteCommandHandler
    : IRequestHandler<CrearEstudianteCommand, Guid>
{
    private readonly IEstudianteQueryRepository _estudianteQueryRepository;
    private readonly IEstudianteRepository _estudianteRepository;

    public EditarEstudianteCommandHandler(IEstudianteQueryRepository estudianteQueryRepository, IEstudianteRepository estudianteRepository)
    {
        _estudianteQueryRepository = estudianteQueryRepository;
        _estudianteRepository = estudianteRepository;
    }
     

    public async Task<Guid> Handle(
        CrearEstudianteCommand request,
        CancellationToken cancellationToken)
    {
        if (await _estudianteQueryRepository.ObtenerPorUserIdAsync(request.UserId) != null)
            throw new NoEncontradoException("Estudiante");

        Estudiante estudiante = new Estudiante(request.UserId, request.Nombre, request.Apellido);
        await _estudianteRepository.CrearAsync(estudiante);
        return estudiante.Id;
    }
}
