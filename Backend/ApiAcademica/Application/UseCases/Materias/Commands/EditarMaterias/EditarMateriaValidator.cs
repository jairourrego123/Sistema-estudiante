using Domain;
using FluentValidation;


namespace Application.UseCases.Materias.Commands.EditarMaterias;

public class EditarMateriaValidator : AbstractValidator<EditarMateriaCommand>
{
    public EditarMateriaValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage(string.Format(Messages.ValorVacio, "Id")); ;
        RuleFor(x => x.Nombre).NotEmpty().WithMessage(string.Format(Messages.ValorVacio, "Nombre"));
        RuleFor(x => x.ProfesorId).NotEmpty().WithMessage(string.Format(Messages.ValorVacio, "Profesor"));

    }
}
