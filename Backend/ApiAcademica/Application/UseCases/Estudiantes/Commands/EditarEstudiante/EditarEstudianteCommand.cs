using MediatR;

namespace Application.UseCases.Estudiantes.Commands.EditarEstudiante;

public record EditarEstudianteCommand(
    Guid Id,
    string Nombre,
    string Apellido
) : IRequest<Unit>;