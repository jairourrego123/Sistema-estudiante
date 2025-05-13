using MediatR;


namespace Application.UseCases.Profesores.Queries.ListarProfesores
{
    public record ListarProfesoresQuery() : IRequest<List<Profesor>>;

}
