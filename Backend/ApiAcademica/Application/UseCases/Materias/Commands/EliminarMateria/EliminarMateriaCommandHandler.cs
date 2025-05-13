using Domain.Exceptions;
using Domain.Ports.IMateriaRepository;
using MediatR;

namespace Application.UseCases.Materias.Commands.EliminarMateria;

public class EliminarMateriaCommandHandler
        : IRequestHandler<EliminarMateriaCommand, Unit>
{
    private readonly IMateriaRepository _materiaRepository;
    private readonly IMateriaQueryRepository _materiaQueryRepository;

    public EliminarMateriaCommandHandler(
        IMateriaRepository materiaRepository,
        IMateriaQueryRepository materiaQueryRepository)
    {
        _materiaRepository = materiaRepository;
        _materiaQueryRepository = materiaQueryRepository;
    }

    public async Task<Unit> Handle(
        EliminarMateriaCommand request,
        CancellationToken cancellationToken)
    {
        Materia materia = await _materiaQueryRepository.ObtenerPorIdAsync(request.Id)
            ?? throw new NoEncontradoException("Materia");

        await _materiaRepository.EliminarAsync(materia.Id);
        return Unit.Value;
    }
}