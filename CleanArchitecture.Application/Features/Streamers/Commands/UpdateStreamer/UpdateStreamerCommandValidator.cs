using FluentValidation;

namespace CleanArchitecture.Application.Features.Streamers.Commands.UpdateStreamer
{
    public class UpdateStreamerCommandValidator : AbstractValidator<UpdateStreamerCommand>
    {
        public UpdateStreamerCommandValidator() 
        {
            RuleFor(property => property.Name)
                .NotNull().WithMessage("{Nombre} no permite nulos");
            RuleFor(property => property.Url)
                .NotNull().WithMessage("{Url} no permite nulos");

        }
    }
}
