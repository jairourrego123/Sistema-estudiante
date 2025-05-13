

using Application.Ports;
using Infrastructure.Adapters.GenericRepository;
using Infrastructure.DataSource;

namespace Infrastructure.Adapters
{
    [Repository]
    public class UnidadDeTrabajo : IUnidadDeTrabajo
    {
        private readonly DataContext _context;
        public UnidadDeTrabajo(DataContext context) => _context = context;
        public Task CommitAsync() => _context.SaveChangesAsync();
    }
}
