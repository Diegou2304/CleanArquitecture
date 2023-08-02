using FluentValidation;
using MediatR;
using ValidationException = CleanArchitecture.Application.Exceptions.ValidationException;

namespace CleanArchitecture.Application.Behaviours
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        //Capturar todas las validaciones que vimos en la aplicación
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(_validators.Select(validation => validation.ValidateAsync(context, cancellationToken)));

                var failures = validationResults.SelectMany(r => r.Errors).Where(f => f!= null).ToList();

                if(failures.Count != 0)
                {
                    throw new ValidationException(failures);
                }

            }
            return await next();
        }
    }
}
