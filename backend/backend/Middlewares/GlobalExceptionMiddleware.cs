using System.Net;
using System.Text.Json;
using backend.Exceptions;

namespace backend.Middlewares
{
    public class GlobalExceptionMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(ILogger<GlobalExceptionMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            HttpStatusCode status = HttpStatusCode.InternalServerError;
            string message = "An unexpected error occurred.";

            if (ex is BadRequestException)
            {
                status = HttpStatusCode.BadRequest;
                message = ex.Message;
            }
            else if (ex is NotFoundException)
            {
                status = HttpStatusCode.NotFound;
                message = ex.Message;
            }
            else if (ex is UnauthorizedException)
            {
                status = HttpStatusCode.Unauthorized;
                message = ex.Message;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;

            var result = JsonSerializer.Serialize(new ErrorResponse
            {
                StatusCode = (int)status,
                Message = message,
                Details = ex.Message
            });

            return context.Response.WriteAsync(result);
        }
    }
}