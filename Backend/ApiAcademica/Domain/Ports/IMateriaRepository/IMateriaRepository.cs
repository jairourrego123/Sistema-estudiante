namespace Domain.Ports.IMateriaRepository;

public interface IMateriaRepository
{
    Task CrearAsync(Materia materia);
    Task ActualizarAsync(Materia materia);
    Task EliminarAsync(Guid id);
    Task GuardarCambiosAsync();
}
