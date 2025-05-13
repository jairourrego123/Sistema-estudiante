using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class InscripcionConfiguration : IEntityTypeConfiguration<Inscripcion>
{
    public void Configure(EntityTypeBuilder<Inscripcion> builder)
    {
        builder.ToTable("Inscripciones");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.FechaGrabacion)
            .IsRequired();

        builder.HasOne(i => i.Estudiante)
            .WithMany(e => e.Inscripciones)
            .HasForeignKey(i => i.EstudianteId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(i => i.Materia)
            .WithMany()
            .HasForeignKey(i => i.MateriaId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
