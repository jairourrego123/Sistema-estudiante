using Domain;
using FluentValidation;

namespace Application.UseCases.Materias.Commands.CrearMaterias;

public class CrearMateriaValidator : AbstractValidator<CrearMateriaCommand>
{
    public CrearMateriaValidator()
    {
        RuleFor(x => x.Nombre).NotEmpty().WithMessage(string.Format(Messages.ValorVacio, "Nombre")); 
        RuleFor(x => x.ProfesorId).NotEmpty().WithMessage(string.Format(Messages.ValorVacio, "Profesor"));
    }
}