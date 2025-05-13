using Application.Dtos;
using MediatR;


namespace Application.UseCases.Materias.Queries;

public record ListarMateriasQuery() : IRequest<List<MateriaConProfesorDto>>;
