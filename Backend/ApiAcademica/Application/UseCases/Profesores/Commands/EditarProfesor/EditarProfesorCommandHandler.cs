using Domain.Ports.IProfesorRepository;
using MediatR;
using Domain.Exceptions;

namespace Application.UseCases.Profesores.Commands.EditarProfesor;

public class EditarProfesorCommandHandler :
     IRequestHandler<EditarProfesorCommand, Unit>
{
    private readonly IProfesorRepository _profesorRepository;
    private readonly IProfesorQueryRepository _profesorQueryRepository;

    public EditarProfesorCommandHandler(
        IProfesorRepository profesorRepository,
        IProfesorQueryRepository profesorQueryRepository)
    {
        _profesorRepository = profesorRepository;
        _profesorQueryRepository = profesorQueryRepository;
    }

    public async Task<Unit> Handle(EditarProfesorCommand request, CancellationToken ct)
    {
        Profesor profesor = await _profesorQueryRepository.ObtenerPorIdAsync(request.Id)
            ?? throw new NoEncontradoException("Profesor");
        profesor.CambiarNombre(request.Nombre);
        await _profesorRepository.ActualizarAsync(profesor);
        return Unit.Value;
    }
}
