using Domain.Entities;
using Domain.Exceptions;
using Domain.Ports.IInscripcionRepository;
using Infrastructure.Adapters.GenericRepository;
using Infrastructure.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Adapters.InscripcionRepository;

[Repository]
public class InscripcionRepository : IInscripcionRepository
{
    private readonly IGenericRepository<Inscripcion> _inscripcionRepository;

    public InscripcionRepository(IGenericRepository<Inscripcion> inscripcionRepository)
    {
        _inscripcionRepository = inscripcionRepository;
    }

    public Task CrearAsync(Inscripcion inscripcion)
    {
        return _inscripcionRepository.AddAsync(inscripcion);
    }

    public async Task EliminarAsync(Guid inscripcionId)
    {
        Inscripcion inscripcion = await _inscripcionRepository.GetOneAsync(inscripcionId)
               ?? throw new NoEncontradoException("Inscripcion");
        await _inscripcionRepository.DeleteAsync(inscripcion);
    }

    public Task GuardarCambiosAsync()
        => _inscripcionRepository.SaveChangesAsync();
}