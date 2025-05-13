
using Domain.Exceptions;
using Domain.Ports.IMateriaRepository;
using Infrastructure.Adapters.GenericRepository;
using Infrastructure.Ports;

namespace Infrastructure.Adapters.MateriaRepository;

[Repository]
public class MateriaRepository : IMateriaRepository
{
    private readonly IGenericRepository<Materia> _materiaRepository;

    public MateriaRepository(IGenericRepository<Materia> materiaRepository)
    {
        _materiaRepository = materiaRepository;
    }

    public Task CrearAsync(Materia materia)
    {
        return _materiaRepository.AddAsync(materia);
    }

    public Task ActualizarAsync(Materia materia)
    {
        return _materiaRepository.UpdateAsync(materia);
    }

    public async Task EliminarAsync(Guid id)
    {
        Materia materia = await _materiaRepository.GetOneAsync(id)
                ?? throw new NoEncontradoException ("Materia");
        await _materiaRepository.DeleteAsync(materia);
    }

    public Task GuardarCambiosAsync()
        => _materiaRepository.SaveChangesAsync();
}
