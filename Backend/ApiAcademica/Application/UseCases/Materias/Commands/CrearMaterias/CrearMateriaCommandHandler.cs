using Domain.Ports.IMateriaRepository;
using Domain.Ports.IProfesorRepository;
using Domain.Exceptions;
using MediatR;

namespace Application.UseCases.Materias.Commands.CrearMaterias;

public class CrearMateriaCommandHandler
       : IRequestHandler<CrearMateriaCommand, Guid>
{
    private readonly IMateriaRepository _materiaRepository;
    private readonly IProfesorRepository _profesorRepository;

    private readonly IProfesorQueryRepository _profesorQueryRepository;

    public CrearMateriaCommandHandler(
        IMateriaRepository materiaRepository,
        IProfesorRepository profesorRepository,
        IProfesorQueryRepository profesorQueryRepository)
    {
        _materiaRepository = materiaRepository;
        _profesorQueryRepository = profesorQueryRepository;
        _profesorRepository = profesorRepository;
    }

    public async Task<Guid> Handle(CrearMateriaCommand request,CancellationToken cancellationToken)
    {
        Profesor profesor = await ValidarProfesor(request);
        Materia materia = new Materia(request.Nombre, profesor);
        profesor.AgregarMateria(materia);
        await _materiaRepository.CrearAsync(materia);
        await _profesorRepository.ActualizarAsync(profesor);
        return materia.Id;
    }

    private async Task<Profesor> ValidarProfesor(CrearMateriaCommand request)
    {
        return await _profesorQueryRepository.ObtenerPorIdAsync(request.ProfesorId)
            ?? throw new NoEncontradoException("Profesor");
    }
}