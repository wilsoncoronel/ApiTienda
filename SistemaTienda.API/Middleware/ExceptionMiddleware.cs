using Microsoft.EntityFrameworkCore;
using SistemaTienda.API.Utilidad;
using System.Net;
using System.Text.Json;
using SistemaTienda.API.Exceptions;

namespace SistemaTienda.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
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
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            HttpStatusCode statusCode;
            string message;
            switch (ex) {
                // 400 Bad Request
                case BadRequestException:
                    statusCode = HttpStatusCode.BadRequest;
                    message = ex.Message;
                    break;

                // 401 Unauthorized
                case UnauthorizedException:
                    statusCode = HttpStatusCode.Unauthorized;
                    message = ex.Message;
                    break;

                // 404 Not Found
                case NotFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    message = ex.Message;
                    break;

                // 409 Conflict
                case ConflictException:
                    statusCode = HttpStatusCode.Conflict;
                    message = ex.Message;
                    break;

                // UnauthorizedAccessException
                case UnauthorizedAccessException:
                    statusCode = HttpStatusCode.Unauthorized;
                    message = ex.Message;
                    break;

                // Error de EF Core
                case DbUpdateException:
                    statusCode = HttpStatusCode.InternalServerError;
                    message = "Ocurrió un error al guardar la información en la base de datos.";

                    _logger.LogError(
                        ex,
                        "Error de Entity Framework Core");
                    break;

                // Cualquier otro error
                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    message = "Ocurrió un error interno en el servidor.";

                    _logger.LogError(
                        ex,
                        "Error no controlado en la API");
                    break;

            }
            var response = new Response<object>
            {
                status = false,
                Value = null,
                msg = message
            };

            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";

            var json = JsonSerializer.Serialize(response);

            await context.Response.WriteAsync(json);
        }
    }
}
