using MediatR;

namespace Application.UseCases.Estudiantes.Commands.EliminarEstudiante;
    
public record EliminarEstudianteCommand(Guid Id) : IRequest<Unit>;
