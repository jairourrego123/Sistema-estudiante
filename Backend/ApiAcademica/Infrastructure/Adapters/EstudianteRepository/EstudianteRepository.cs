using Domain.Entities;
using Domain.Ports;
using Domain.Ports.IEstudianteRepository;
using Infrastructure.Adapters.GenericRepository;
using Infrastructure.Ports;
using Domain.Exceptions;
namespace Infrastructure.Adapters.EstudianteRepository
{
    [Repository]
    public class EstudianteRepository : IEstudianteRepository
    {
        private readonly IGenericRepository<Estudiante> _generic;
        public EstudianteRepository(IGenericRepository<Estudiante> generic)
            => _generic = generic;

        public async Task CrearAsync(Estudiante estudiante)
            => await _generic.AddAsync(estudiante);

        public async Task ActualizarAsync(Estudiante estudiante)
            => await _generic.UpdateAsync(estudiante);

        public async Task EliminarAsync(Guid id)
        {
            var e = await _generic.GetOneAsync(id)
                    ?? throw new NoEncontradoException("Estudiante");
            await _generic.DeleteAsync(e);
        }


        public async Task GuardarCambiosAsync()
            => await _generic.SaveChangesAsync();
    }
}
