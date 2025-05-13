using Domain;
using FluentValidation;

namespace Application.UseCases.Inscripciones.Command.DesinscribirMaterias;

public class DesinscribirMateriaCommandValidator
      : AbstractValidator<DesinscribirMateriaCommand>
{
    public DesinscribirMateriaCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage(string.Format(Messages.ValorVacio, "Usuario"));

        RuleFor(x => x.MateriaId)
            .NotEmpty().WithMessage(string.Format(Messages.ValorVacio, "Materia"));
    }
}
