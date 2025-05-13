using MediatR;


namespace Application.UseCases.Inscripciones.Command.DesinscribirMaterias;

public record DesinscribirMateriaCommand(string UserId, Guid MateriaId) : IRequest<Unit>;
