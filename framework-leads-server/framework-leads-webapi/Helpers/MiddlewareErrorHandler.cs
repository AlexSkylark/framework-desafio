using System.Net;
using System.Text.Json;

namespace FrameworkLeads.Helpers
{
    public class MalformedDataException : Exception
    {
        public MalformedDataException() : base() { }
        public MalformedDataException(string message)
            : base(message)
        { }
    }

    public class MiddlewareErrorHandler
    {
        private readonly RequestDelegate _next;

        public MiddlewareErrorHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                // tratamento de erros global
                if (error is NullReferenceException ||
                    error is InvalidCastException ||
                    error is MalformedDataException)
                {
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                }

                else if (error is KeyNotFoundException)
                {
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                }

                else
                {
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                }

                var result = JsonSerializer.Serialize(new { message = error?.Message });
                await response.WriteAsync(result);
            }
        }
    }
}



