using MediatR;


namespace Application.UseCases.Inscripciones.Command.InscribirMaterias;

public record InscribirMateriasCommand(string UserId, List<Guid> MateriaIds) : IRequest<Unit>;
