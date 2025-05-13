using MediatR;
using Domain.Entities;
namespace Application.UseCases.Estudiantes.Queries.ObtenerEstudiantePorId;

public record ObtenerEstudiantePorIdQuery(Guid Id) : IRequest<Estudiante?>;
