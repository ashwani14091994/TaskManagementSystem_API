using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace TaskManagementSystem_API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = HttpStatusCode.InternalServerError; // Default status code
            var message = "An unexpected error occurred.";

            if (exception is ApplicationException)
            {
                statusCode = HttpStatusCode.BadRequest;
                message = exception.Message;
            }
            // Add more custom exception types and their respective status codes here

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var result = new
            {
                StatusCode = (int)statusCode,
                Message = message,
                Detailed = exception.StackTrace
            };

            return context.Response.WriteAsJsonAsync(result);
        }
    }

}
