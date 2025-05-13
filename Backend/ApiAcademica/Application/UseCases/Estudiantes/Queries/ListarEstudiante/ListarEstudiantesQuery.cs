using MediatR;
using Domain.Entities;
namespace Application.UseCases.Estudiantes.Queries.ListarEstudiante;

public record ListarEstudiantesQuery() : IRequest<List<Estudiante>>;
