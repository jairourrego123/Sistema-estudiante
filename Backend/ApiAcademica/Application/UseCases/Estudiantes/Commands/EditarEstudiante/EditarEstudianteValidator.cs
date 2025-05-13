using Domain;
using FluentValidation;

namespace Application.UseCases.Estudiantes.Commands.EditarEstudiante;

public class EditarEstudianteValidator
    : AbstractValidator<EditarEstudianteCommand>
{
    public EditarEstudianteValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage(string.Format(Messages.ValorVacio, "Id")); 
        RuleFor(x => x.Nombre).NotEmpty().WithMessage(string.Format(Messages.ValorVacio, "Nombre")); 
        RuleFor(x => x.Apellido).NotEmpty().WithMessage(string.Format(Messages.ValorVacio, "Apellido"));
    }
}