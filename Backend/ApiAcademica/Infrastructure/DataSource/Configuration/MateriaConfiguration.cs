using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class MateriaConfiguration : IEntityTypeConfiguration<Materia>
{
    public void Configure(EntityTypeBuilder<Materia> builder)
    {
        builder.ToTable("Materias");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Nombre)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(m => m.Creditos)
            .IsRequired();

        builder.Property(m => m.FechaGrabacion)
            .IsRequired();

        builder.HasOne(m => m.Profesor)
            .WithMany(p => p.Materias)
            .HasForeignKey(m => m.ProfesorId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
