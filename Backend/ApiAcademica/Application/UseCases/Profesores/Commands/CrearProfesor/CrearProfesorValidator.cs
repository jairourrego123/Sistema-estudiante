using Domain;
using FluentValidation;

namespace Application.UseCases.Profesores.Commands.CrearProfesor;

public class CrearProfesorValidator : AbstractValidator<CrearProfesorCommand>
{
    public CrearProfesorValidator()
    {
        RuleFor(x => x.Nombre).NotEmpty().WithMessage(string.Format(Messages.ValorVacio,"Nombre"));
    }
}
