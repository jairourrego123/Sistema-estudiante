using Domain;
using FluentValidation;


namespace Application.UseCases.Inscripciones.Command.InscribirMaterias;

public class InscribirMateriasCommandValidator
      : AbstractValidator<InscribirMateriasCommand>
{
    public InscribirMateriasCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage(string.Format(Messages.ValorVacio, "Usuario"));

        RuleFor(x => x.MateriaIds)
            .NotEmpty().WithMessage(string.Format(Messages.ValorVacio, "Materias")); 
           
    }
}
