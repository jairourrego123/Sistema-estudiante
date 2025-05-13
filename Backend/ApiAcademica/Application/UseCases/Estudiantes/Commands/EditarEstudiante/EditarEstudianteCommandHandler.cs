using Domain.Exceptions;
using Domain.Entities;
using Domain.Ports.IEstudianteRepository;
using MediatR;

namespace Application.UseCases.Estudiantes.Commands.EditarEstudiante;

public class EditarEstudianteCommandHandler
    : IRequestHandler<EditarEstudianteCommand, Unit>  
{
    private readonly IEstudianteQueryRepository _estudianteQueryRepository;
    private readonly IEstudianteRepository _estudianteRepository;

    public EditarEstudianteCommandHandler(IEstudianteQueryRepository estudianteQueryRepository, IEstudianteRepository estudianteRepository)
    {
        _estudianteQueryRepository = estudianteQueryRepository;
        _estudianteRepository = estudianteRepository;
    }
    public async Task<Unit> Handle(
        EditarEstudianteCommand request,
        CancellationToken cancellationToken)
    {
       Estudiante estudiante = await _estudianteQueryRepository.ObtenerPorIdAsync(request.Id)
            ?? throw new NoEncontradoException("Estudiante");


        estudiante.CambiarNombre(request.Nombre);
        estudiante.CambiarApellido(request.Apellido);

        await _estudianteRepository.ActualizarAsync(estudiante);
        return Unit.Value;  
    }
}
