
using Domain.Exceptions;
using Domain.Ports.IProfesorRepository;
using MediatR;

namespace Application.UseCases.Profesores.Commands.EliminarProfesor;

public class EliminarProfesorCommandHandler :
        IRequestHandler<EliminarProfesorCommand, Unit>
{
    private readonly IProfesorRepository _repo;
    private readonly IProfesorQueryRepository _qr;

    public EliminarProfesorCommandHandler(
        IProfesorRepository repo,
        IProfesorQueryRepository qr)
    {
        _repo = repo;
        _qr = qr;
    }

    public async Task<Unit> Handle(EliminarProfesorCommand request, CancellationToken ct)
    {
        Profesor profesor = await _qr.ObtenerPorIdAsync(request.Id)
            ?? throw new NoEncontradoException("Profesor");
        await _repo.EliminarAsync(profesor.Id);
        return Unit.Value;
    }
}