
using FluentValidation;

namespace CleanArchitecture.Application.Features.Streamers.Commands.DeleteStreamer
{
    public class DeleteStreamerCommandValidator : AbstractValidator<DeleteStreamerCommand>
    {
        public DeleteStreamerCommandValidator() 
        {
            RuleFor(property => property.Id)
                .NotEmpty().WithMessage("El campo ID no puede estar vacio")
                .NotNull();
        
        }
    }
}
