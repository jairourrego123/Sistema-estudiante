using Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.DataSource;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{

    protected override void OnModelCreating(ModelBuilder? modelBuilder)
    {
        if (modelBuilder is null)
        {
            return;
        }

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Estudiante> Estudiantes => Set<Estudiante>();
    public DbSet<Materia> Materias => Set<Materia>();
    public DbSet<Profesor> Profesores => Set<Profesor>();
    public DbSet<Inscripcion> Inscripciones => Set<Inscripcion>();
}

