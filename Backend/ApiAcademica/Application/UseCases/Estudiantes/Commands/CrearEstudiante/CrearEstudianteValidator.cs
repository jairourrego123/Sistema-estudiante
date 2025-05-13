using Domain;
using FluentValidation;

namespace Application.UseCases.Estudiantes.Commands.CrearEstudiante;

public class EditarEstudianteValidator
    : AbstractValidator<CrearEstudianteCommand>
{
    public EditarEstudianteValidator()
    {
        RuleFor(x => x.Nombre).NotEmpty().WithMessage(string.Format(Messages.ValorVacio, "Nombre")); 
        RuleFor(x => x.Apellido).NotEmpty().WithMessage(string.Format(Messages.ValorVacio, "Apellido")); 
    }
}
