using DogHouseService.Web.Middlewares.PresentationModels;
using System.Net;
using System.Text.Json;

namespace DogHouseService.Web.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                var errorDetails = new ErrorDetails()
                {
                    StatusCode = httpContext.Response.StatusCode,
                    Message = ex.Message,
                };


                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(errorDetails));

            }
        }
    }

    public static class ExceptionHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
