
using Domain.Ports.IProfesorRepository;
using Domain.Exceptions;
using Infrastructure.Ports;
using Infrastructure.Adapters.GenericRepository;

namespace Infrastructure.Adapters.ProfesorRepository;

[Repository]
public class ProfesorRepository : IProfesorRepository
{
    private readonly IGenericRepository<Profesor> _profesorRepository;
    public ProfesorRepository(IGenericRepository<Profesor> profesorRepository)
        => _profesorRepository = profesorRepository;

    public Task CrearAsync(Profesor p) => _profesorRepository.AddAsync(p);
    public Task ActualizarAsync(Profesor p) => _profesorRepository.UpdateAsync(p);
    public async Task EliminarAsync(Guid id)
    {
        Profesor profesor = await _profesorRepository.GetOneAsync(id)
                 ?? throw new NoEncontradoException("Profesor");
        await _profesorRepository.DeleteAsync(profesor);
    }
    public Task GuardarCambiosAsync() => _profesorRepository.SaveChangesAsync();
}

