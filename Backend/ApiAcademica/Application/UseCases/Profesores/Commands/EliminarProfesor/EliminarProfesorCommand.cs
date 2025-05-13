using MediatR;

namespace Application.UseCases.Profesores.Commands.EliminarProfesor
{
    public record EliminarProfesorCommand(Guid Id) : IRequest<Unit>;

}
