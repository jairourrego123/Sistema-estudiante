using Domain;
using FluentValidation;

namespace Application.UseCases.Profesores.Commands.EditarProfesor;

public class EditarProfesorValidator : AbstractValidator<EditarProfesorCommand>
{
    public EditarProfesorValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage(string.Format(Messages.ValorVacio, "Id"));
        RuleFor(x => x.Nombre).NotEmpty().WithMessage(string.Format(Messages.ValorVacio, "Nombre"));
    }
}