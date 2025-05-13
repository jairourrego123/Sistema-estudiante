using MediatR;

namespace Application.UseCases.Profesores.Commands.EditarProfesor
{
    public record EditarProfesorCommand(Guid Id, string Nombre) : IRequest<Unit>;

}
