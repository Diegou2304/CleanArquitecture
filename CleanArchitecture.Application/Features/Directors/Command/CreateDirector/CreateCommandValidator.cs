using FluentValidation;

namespace CleanArchitecture.Application.Features.Directors.Command.CreateDirector
{
    public class CreateCommandValidator : AbstractValidator<CreateDirectorCommand>
    {
        public CreateCommandValidator() 
        {
            RuleFor(p => p.Name)
                .NotNull().NotEmpty().WithMessage("Nombre no puede estar vacio o ser nulo");

            RuleFor(p => p.LastName)
                .NotNull().NotEmpty().WithMessage("Nombre no puede estar vacio o ser nulo");
        }
    }
}
