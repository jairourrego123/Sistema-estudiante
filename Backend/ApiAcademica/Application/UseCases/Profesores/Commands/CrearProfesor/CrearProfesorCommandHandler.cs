using Domain.Ports.IProfesorRepository;
using MediatR;


namespace Application.UseCases.Profesores.Commands.CrearProfesor;

public class CrearProfesorCommandHandler :
      IRequestHandler<CrearProfesorCommand, Guid>
{
    private readonly IProfesorRepository _profesorRepository;

    public CrearProfesorCommandHandler(IProfesorRepository profesorRepository)
        => _profesorRepository = profesorRepository;

    public async Task<Guid> Handle(CrearProfesorCommand request, CancellationToken ct)
    {
        Profesor profesor = new Profesor(request.Nombre);
        await _profesorRepository.CrearAsync(profesor);
        return profesor.Id;
    }
}
