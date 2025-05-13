using MediatR;

namespace Application.UseCases.Materias.Commands.EditarMaterias;

public record EditarMateriaCommand(
   Guid Id,
   string Nombre,
   Guid ProfesorId
) : IRequest<Unit>;
