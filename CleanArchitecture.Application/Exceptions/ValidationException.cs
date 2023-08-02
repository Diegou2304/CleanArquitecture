using FluentValidation.Results;

namespace CleanArchitecture.Application.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public IDictionary<string, string[]> Errors { get; }
        public ValidationException() : base("Se presentaron uno o mas errores de validación") 
        {
            Errors = new Dictionary<string, string[]>();

        }

        public ValidationException(IEnumerable<ValidationFailure> failures) : this()
        {
            Errors = failures.GroupBy(error => error.PropertyName, error => error.ErrorMessage)
                            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());

        }

    }
}
