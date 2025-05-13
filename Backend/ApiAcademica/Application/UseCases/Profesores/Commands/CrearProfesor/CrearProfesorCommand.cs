using MediatR;

namespace Application.UseCases.Profesores.Commands.CrearProfesor;

public record CrearProfesorCommand(string Nombre) : IRequest<Guid>;

