using MediatR;


namespace Application.UseCases.Profesores.Queries.ObtenerProfesorPorIdQuery;

public record ObtenerProfesorPorIdQuery(Guid Id) : IRequest<Profesor?>;
