using CleanArchitecture.API.Errors;
using CleanArchitecture.Application.Exceptions;
using Newtonsoft.Json;
using SendGrid.Helpers.Errors.Model;
using System.Diagnostics;
using System.Net;
using System.Text.Json;

namespace CleanArchitecture.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, 
                                   ILogger<ExceptionMiddleware> logger, 
                                   IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                var statusCode = (int)HttpStatusCode.InternalServerError;
                var result = string.Empty;

                switch(ex)
                {
                    case Application.Exceptions.NotFoundException notFoundException:
                        statusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case ValidationException validationException:
                        statusCode = (int)HttpStatusCode.BadRequest;
                        var validationJson = JsonConvert.SerializeObject(validationException.Errors);
                        result = JsonConvert.SerializeObject(new ErrorException(statusCode, ex.Message));
                        break;

                }
                var response = _env.IsDevelopment() 
                    ? new ErrorException((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace)
                    : new ErrorException((int)HttpStatusCode.InternalServerError);

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                var json = System.Text.Json.JsonSerializer.Serialize(response,options);

                //Envia la respuesta al cliente
                await context.Response.WriteAsync(json);
            }
        }
    }
}
