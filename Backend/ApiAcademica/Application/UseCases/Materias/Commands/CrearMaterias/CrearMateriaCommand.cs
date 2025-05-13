using MediatR;


namespace Application.UseCases.Materias.Commands.CrearMaterias;

public record CrearMateriaCommand(
     string Nombre,
     Guid ProfesorId
 ) : IRequest<Guid>;
