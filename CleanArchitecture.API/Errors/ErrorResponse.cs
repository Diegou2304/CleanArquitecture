namespace CleanArchitecture.API.Errors
{
    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }

        public ErrorResponse(int statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageStatusCode(statusCode);
        }

        private string GetDefaultMessageStatusCode(int statusCode)
        {

            return statusCode switch
            {
                400 => "Error de cliente",
                401 => "No se encontró el recurso solicitado",
                404 => "No se encontro el recurso solicitado",
                500 => "Error del lado del servidor",
                _ => string.Empty
            };
        }
    }
}
