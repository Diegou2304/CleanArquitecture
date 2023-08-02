

using CleanArchitecture.Application.Models.Identity;
using FluentValidation;

namespace CleanArchitecture.Identity.Services
{
    public class AuthServiceRegisterValidator : AbstractValidator<RegistrationRequest>
    {
        public AuthServiceRegisterValidator() 
        {
            RuleFor(property => property.Password)
                .NotEmpty().WithMessage("Password no puede estar vacio");

            RuleFor(property => property.Password)
                .Custom((pass, context) =>
                {
                    if(pass.Length < 30)
                    {
                        context.AddFailure("La contraseña tiene que tener mas de 10 caracteres");
                    }

                });

        }
    }
}
