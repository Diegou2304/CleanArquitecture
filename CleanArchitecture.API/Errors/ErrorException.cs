namespace CleanArchitecture.API.Errors
{
    public class ErrorException : ErrorResponse
    {
        public string? Details { get; set; }
        public ErrorException(int statusCode, string? message = null, string? details = null) : base(statusCode, message)
        {
            Details = details;
        }
    }
}
