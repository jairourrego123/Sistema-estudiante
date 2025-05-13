using MediatR;


namespace Application.UseCases.Materias.Commands.EliminarMateria;

public record EliminarMateriaCommand(Guid Id) : IRequest<Unit>;
