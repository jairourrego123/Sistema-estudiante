using MediatR;

namespace Application.UseCases.Estudiantes.Commands.CrearEstudiante;

public record CrearEstudianteCommand(
    string UserId,
    string Nombre,
    string Apellido
) : IRequest<Guid>;
