using Application.Dtos;
using MediatR;

namespace Application.UseCases.Estudiantes.Queries.ObtenerCompañeros;

public record ListarCompanerosQuery(string UserId) : IRequest<List<NombreDto>>;
