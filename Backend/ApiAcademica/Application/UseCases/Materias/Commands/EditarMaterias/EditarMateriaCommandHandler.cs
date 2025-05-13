using Domain.Exceptions;
using Domain.Ports.IMateriaRepository;
using Domain.Ports.IProfesorRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Materias.Commands.EditarMaterias;

public class EditarMateriaCommandHandler
       : IRequestHandler<EditarMateriaCommand, Unit>
{
    private readonly IMateriaRepository _materiaRepository;
    private readonly IMateriaQueryRepository _queryRepositorio;
    private readonly IProfesorRepository _profesorRepository;
    private readonly IProfesorQueryRepository _profesorQueryRepository;

    public EditarMateriaCommandHandler(
        IMateriaRepository materiaRepository,
        IMateriaQueryRepository materiaQueryRepository,
        IProfesorRepository profesorRepository,
        IProfesorQueryRepository profesorQueryRepository    
)
    {
        _materiaRepository = materiaRepository;
        _queryRepositorio = materiaQueryRepository;
        _profesorRepository = profesorRepository;
        _profesorQueryRepository = profesorQueryRepository;
    }

    public async Task<Unit> Handle(EditarMateriaCommand request,CancellationToken cancellationToken)
    {
        Materia materia = await _queryRepositorio.ObtenerPorIdAsync(request.Id)
            ?? throw new NoEncontradoException("Materia");
        materia.CambiarNombre(request.Nombre);
        await CambiarProfesor(request, materia);
        await _materiaRepository.ActualizarAsync(materia);
        return Unit.Value;
    }

    private async Task CambiarProfesor(EditarMateriaCommand request, Materia materia)
    {
        if (materia.ProfesorId != request.ProfesorId)
        {
            (Profesor profesorAnterior, Profesor profesorNuevo) = await ObtenerInformacionProfesores(request, materia);
            profesorAnterior.RemoverMateria(materia.Id);
            profesorNuevo.AgregarMateria(materia);
            materia.AsignarProfesor(profesorNuevo);
            await _profesorRepository.ActualizarAsync(profesorAnterior);
            await _profesorRepository.ActualizarAsync(profesorNuevo);
        }
    }

    private async Task<(Profesor profesorAnterior, Profesor profesorNuevo)> ObtenerInformacionProfesores(EditarMateriaCommand request, Materia materia)
    {
        Profesor profesorAnterior = await _profesorQueryRepository.ObtenerPorIdAsync(materia.ProfesorId)
            ?? throw new NoEncontradoException("Profesor anterior");

        Profesor profesorNuevo = await _profesorQueryRepository.ObtenerPorIdAsync(request.ProfesorId)
            ?? throw new NoEncontradoException("Profesor nuevo");
        return (profesorAnterior, profesorNuevo);
    }
}
